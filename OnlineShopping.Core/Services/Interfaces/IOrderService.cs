namespace OnlineShopping.Core.Services.Interfaces
{
    public interface IOrderService
    {
        Task<RestDto<OrderDto?>> CreateOrderAsync(string customerId);
        Task<RestDto<OrderDto?>> GetOrderByIdAsync(int id);
        Task<RestDto<List<OrderDto>>> GetOrdersByCustomerIdAsync(string customerId);
    }
}
