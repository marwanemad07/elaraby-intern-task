namespace OnlineShopping.Core.Services.Implementations
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly AppDbContext _context;

        public OrderService(IUnitOfWork unitOfWork, 
            IMapper mapper,
            AppDbContext context)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _context = context;
        }

        public async Task<RestDto<OrderDto?>> CreateOrderAsync(string customerId)
        {
            var cart = await _unitOfWork.ShoppingCarts.GetActiveCartAsync(customerId);
            if(cart == null || cart.CartItems.Count == 0)
            {
                var badRequestResult = new RestDto<OrderDto?>(StatusCodes.Status400BadRequest,null, "User has no cart items");
                return badRequestResult;
            }

            // TODO: do this using AutoMapper
            var order = new Order
            {
                CustomerId = customerId,
                OrderDate = DateTime.Now,
                OrderItems = cart.CartItems.Select(x => new OrderItem
                {
                    ProductId = x.ProductId,
                    Quantity = x.Quantity,
                    Price = x.Product.Price
                }).ToList()
            };

            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var createdOrder = await _unitOfWork.Orders.AddAsync(order);

                    await DecreaseProductsOrdered(createdOrder);

                    // Deactivate the cart
                    cart.IsActive = false;
                    _unitOfWork.ShoppingCarts.Update(cart);

                    _unitOfWork.Complete();
                    await transaction.CommitAsync();

                    var dto = _mapper.Map<Order, OrderDto>(createdOrder);
                    var result = new RestDto<OrderDto?>(StatusCodes.Status201Created, dto);
                    return result;
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    return new RestDto<OrderDto?>(StatusCodes.Status500InternalServerError, null, ex.Message);
                }
            }
        }

        public async Task<RestDto<OrderDto?>> GetOrderByIdAsync(int id)
        {
            var order = await _unitOfWork.Orders.GetOrderByIdAsync(id);
            if (order == null)
            {
                var notFoundResult = new RestDto<OrderDto?>(StatusCodes.Status404NotFound);
                return notFoundResult;
            }

            var dto = _mapper.Map<Order, OrderDto>(order);
            var result = new RestDto<OrderDto?>(StatusCodes.Status200OK, dto);
            return result;
        }

        public async Task<RestDto<List<OrderDto>>> GetOrdersByCustomerIdAsync(string customerId)
        {
            var orders = await _unitOfWork.Orders.GetOrdersByCustomerIdAsync(customerId);
            var dtos = _mapper.Map<List<Order>, List<OrderDto>>(orders);
            var result = new RestDto<List<OrderDto>>(StatusCodes.Status200OK, dtos);
            return result;
        }

        private async Task DecreaseProductsOrdered(Order order)
        {
            foreach (var orderItem in order.OrderItems)
            {
                var product = await _unitOfWork.Products.GetByIdAsync(orderItem.ProductId);
                if (product == null)
                {
                    throw new InvalidOperationException($"Product with ID {orderItem.ProductId} not found.");
                }

                if (product.Quantity < orderItem.Quantity)
                {
                    throw new InvalidOperationException($"Not enough quantity for product {product.Name}. Available: {product.Quantity}, requested: {orderItem.Quantity}");
                }

                product.Quantity -= orderItem.Quantity;

                _unitOfWork.Products.Update(product);
            }
        }
    }
}
