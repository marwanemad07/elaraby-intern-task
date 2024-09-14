namespace OnlineShopping.Core.Services.Implementations
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ShoppingCartService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<RestDto<ShoppingCartDto?>> AddCartItemAsync(NewCartItemDto newCartItem, string userId)
        {
            var cart = await GetActiveCartOrCreateNew(userId);
            
            var cartItemEntity = _mapper.Map<NewCartItemDto, CartItem>(newCartItem);
            cartItemEntity.ShoppingCartId = cart.Id;

            var existingCartItem = cart.CartItems.FirstOrDefault(ci => ci.ProductId == cartItemEntity.ProductId);
            var createdOrUpdatedStatusCode = StatusCodes.Status201Created;

            try
            {
                // TODO: Refactor this to two functions, one for adding and one for updating
                if (existingCartItem != null)
                {
                    existingCartItem.Quantity += newCartItem.Quantity;                  
                    if(existingCartItem.Quantity < 1)
                    {
                        throw new Exception("Quantity must be greater than 0");
                    }

                    createdOrUpdatedStatusCode = StatusCodes.Status200OK;
                    _unitOfWork.CartItems.Update(existingCartItem);
                }
                else
                {
                    await _unitOfWork.CartItems.AddAsync(cartItemEntity);
                }
                _unitOfWork.Complete();
            }
            catch (Exception e)
            { 
                var errorResult = new RestDto<ShoppingCartDto?>(StatusCodes.Status400BadRequest, null, e.Message);
                return errorResult;
            }

            cart = await GetActiveCartOrCreateNew(userId);
            var dto = _mapper.Map<ShoppingCart, ShoppingCartDto>(cart);

            var result = new RestDto<ShoppingCartDto?>(createdOrUpdatedStatusCode, dto);
            return result;
        }

        public async Task<RestDto<ShoppingCartDto?>> GetActiveCartAsync(string userId)
        {
            var cart = await GetActiveCartOrCreateNew(userId);

            var dto = _mapper.Map<ShoppingCart, ShoppingCartDto>(cart);
            var result = new RestDto<ShoppingCartDto?>(StatusCodes.Status200OK, dto);
            return result;
        }

        public async Task<RestDto<ShoppingCartDto?>> RemoveCartItemAsync(int cartItemId, string userId)
        {
            try
            {
                var cartItem = await _unitOfWork.CartItems.GetByIdAsync(cartItemId);
                
                if(cartItem == null)
                {
                    throw new Exception("Cart item not found");
                }

                _unitOfWork.CartItems.Remove(cartItem);
                _unitOfWork.Complete();

                var result = new RestDto<ShoppingCartDto?>(StatusCodes.Status204NoContent, null);
                return result;
            }
            catch (Exception e)
            {
                var errorResult = new RestDto<ShoppingCartDto?>(StatusCodes.Status404NotFound, null, e.Message);
                return errorResult;
            }
        }

        private async Task<ShoppingCart> GetActiveCartOrCreateNew(string userId)
        {
            var cart = await _unitOfWork.ShoppingCarts.GetActiveCartAsync(userId);

            if (cart == null)
            {
                cart = new ShoppingCart
                {
                    UserId = userId,
                    IsActive = true
                };
                cart = await _unitOfWork.ShoppingCarts.AddAsync(cart);
                _unitOfWork.Complete();
            }

            return cart;
        }
    }
}
