namespace OnlineShopping.Infrastructure.Data.Models
{
    public class Product : BaseEntity
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string ImageUrl { get; set; } = null!;
        public int? CategoryId { get; set; }
        public Category? Category { get; set; }
        public List<CartItem> CartItems { get; set; } // This navigation property may be deleted if we wont access the cart items of a product
        public List<OrderItem> OrderItems { get; set; } // This navigation property may be deleted if we wont access the order items of a product
    }
}
