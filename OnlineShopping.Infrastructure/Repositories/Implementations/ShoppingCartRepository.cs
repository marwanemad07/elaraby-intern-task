namespace OnlineShopping.Infrastructure.Repositories.Implementations
{
    public class ShoppingCartRepository : GenericRepository<ShoppingCart>, IShoppingCartRepository
    {
        private readonly AppDbContext _context;

        public ShoppingCartRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<ShoppingCart?> GetActiveCartAsync(string userId)
        {
            var cart = await _context.ShoppingCarts
                .Include(x => x.CartItems)
                .ThenInclude(x => x.Product)
                .Where(x => x.UserId == userId && x.IsActive)
                .FirstOrDefaultAsync();
            return cart;
        }
    }
}
