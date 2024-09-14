namespace OnlineShopping.Infrastructure.Data.Models.Identity
{
    public class AppUser : IdentityUser
    {
        [Required]
        [MinLength(2), MaxLength(100)]
        public string FirstName { get; set; }
        [Required]
        [MinLength(2), MaxLength(100)]
        public string LastName { get; set; }
        [Required]
        public string Street { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string ZipCode { get; set; }
        public List<ShoppingCart> ShoppingCarts { get; set; } 
        public List<Order> Orders { get; set; }
    }
}
