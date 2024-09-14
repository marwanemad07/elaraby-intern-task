namespace OnlineShopping.Infrastructure.Data.Models
{
    public class Order : BaseEntity
    {
        public string CustomerId { get; set; }
        public AppUser Customer { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.UtcNow;
        public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}
