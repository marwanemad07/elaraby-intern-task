namespace OnlineShopping.Core.Helpers.Mapper.MapResolver
{
    public class ProductImageFileResolver : IValueResolver<NewProductDto, Product, string>
    {
        public string Resolve(NewProductDto source, Product destination, string destMember, ResolutionContext context)
        {
            var imageName = Guid.NewGuid().ToString() + Path.GetExtension(source.ImageFile.FileName);
            var imageUrl = $"{ProductImageConstraints.ImagesFolderPath}/{imageName}";
            return imageUrl;
        }
    }
}
