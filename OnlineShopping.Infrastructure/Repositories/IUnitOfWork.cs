namespace OnlineShopping.Infrastructure.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IAccountRepository AccountRepository { get; }
        IGenericRepository<Product> Products { get;}
        IGenericRepository<CartItem> CartItems { get; }
        IGenericRepository<Category> Categories { get; }
        IShoppingCartRepository ShoppingCarts { get; }
        IOrderRepository Orders { get; }

        int Complete();
    }
}
