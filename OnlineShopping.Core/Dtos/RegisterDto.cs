using System.ComponentModel.DataAnnotations;

namespace OnlineShopping.Core.Dtos
{
    public class RegisterDto
    {
        [Required]
        [MinLength(2), MaxLength(100)]
        public string FirstName { get; set; }
        [Required]
        [MinLength(2), MaxLength(100)]
        public string LastName { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Street { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string ZipCode { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
