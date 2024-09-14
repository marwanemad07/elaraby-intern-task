namespace OnlineShopping.Infrastructure.Data.Models
{
    public class ShoppingCart : BaseEntity
    {
        public bool IsActive { get; set; } = true;
        public string UserId { get; set; }
        public AppUser User { get; set; }
        public List<CartItem> CartItems { get; set; } = new();
    }
}
