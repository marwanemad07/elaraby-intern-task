namespace OnlineShopping.Core.Dtos
{
    public class OrderDto
    {
        public int Id { get; set; }
        public string CustomerId { get; set; }
        public DateTime OrderDate { get; set; }
        public List<OrderItemDto> OrderItems { get; set; } = [];
        public decimal TotalPrice { get; set; }
    }
}
