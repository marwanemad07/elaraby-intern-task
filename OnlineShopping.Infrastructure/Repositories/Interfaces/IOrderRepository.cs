namespace OnlineShopping.Infrastructure.Repositories.Interfaces
{
    public interface IOrderRepository : IGenericRepository<Order>
    {
        Task<Order?> GetOrderByIdAsync(int id);
        Task<List<Order>> GetOrdersByCustomerIdAsync(string customerId);
    }
}
