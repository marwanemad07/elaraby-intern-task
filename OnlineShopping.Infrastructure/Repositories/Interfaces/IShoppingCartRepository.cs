namespace OnlineShopping.Infrastructure.Repositories.Interfaces
{
    public interface IShoppingCartRepository : IGenericRepository<ShoppingCart>
    {
        Task<ShoppingCart?> GetActiveCartAsync(string userId);
    }
}
