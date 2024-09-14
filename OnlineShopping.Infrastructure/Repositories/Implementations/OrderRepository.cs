
namespace OnlineShopping.Infrastructure.Repositories.Implementations
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        public readonly AppDbContext _context;
        public OrderRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<Order?> GetOrderByIdAsync(int id)
        {
            var result = await GetOrderWithOrderItemAndProductQuery()
                .FirstOrDefaultAsync(o => o.Id == id);

            return result;
        }

        public async Task<List<Order>> GetOrdersByCustomerIdAsync(string customerId)
        {
            var result = await GetOrderWithOrderItemAndProductQuery()
                .Where(o => o.CustomerId == customerId)
                .ToListAsync();

            return result;
        }

        private IQueryable<Order> GetOrderWithOrderItemAndProductQuery()
        {
            return _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product);
        }
    }
}
