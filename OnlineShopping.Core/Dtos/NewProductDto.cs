using OnlineShopping.Core.Attributes;
using OnlineShopping.Core.Constraints;

namespace OnlineShopping.Core.Dtos
{
    public class NewProductDto
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = null!;
        [Required]
        [MaxLength(300)]
        public string Description { get; set; } = null!;
        [Required]
        public decimal Price { get; set; }
        [Required]
        public int Quantity { get; set; } = 0;
        [Required]
        [AllowedExtensions(ProductImageConstraints.AllowedExtensions)]
        [MaxFileSize(ProductImageConstraints.MaxSize)]
        public IFormFile ImageFile { get; set; } = null!;
        [Required]
        public int CategoryId { get; set; }
    }
}
