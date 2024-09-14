
namespace OnlineShopping.Core.Constraints
{
    public static class ProductImageConstraints
    {
        public const int MaxSize = 2 * 1024 * 1024;
        public const string AllowedExtensions = ".jpg,.jpeg,.png";
        public const string ImagesFolderPath = "Images";
    }
}
