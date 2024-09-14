namespace OnlineShopping.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        public IAccountRepository AccountRepository { get; private set; }
        public IGenericRepository<Product> Products { get; private set; }
        public IGenericRepository<CartItem> CartItems { get; private set; }
        public IGenericRepository<Category> Categories { get; private set; }
        public IShoppingCartRepository ShoppingCarts { get; private set; }
        public IOrderRepository Orders { get; private set; }


        private readonly AppDbContext _context;
        
        public UnitOfWork(AppDbContext context,
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager)
        {
            _context = context;

            AccountRepository = new AccountRepository(userManager, signInManager);
            Products = new GenericRepository<Product>(_context);
            CartItems = new GenericRepository<CartItem>(_context);    
            Categories = new GenericRepository<Category>(_context);
            ShoppingCarts = new ShoppingCartRepository(_context);
            Orders = new OrderRepository(_context);
        }
        public int Complete()
        {
            int recordsChanged = _context.SaveChanges();
            return recordsChanged;
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
