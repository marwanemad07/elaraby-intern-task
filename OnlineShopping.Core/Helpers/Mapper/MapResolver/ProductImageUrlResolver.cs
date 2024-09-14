using Microsoft.Extensions.Configuration;

namespace OnlineShopping.Core.Helpers.Mapper.MapResolver
{
    public class ProductImageUrlResolver<T1, T2> : IValueResolver<T1, T2, string>
    {
        private readonly IConfiguration _configuration;

        public ProductImageUrlResolver(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string Resolve(T1 source, T2 destination, string destMember, ResolutionContext context)
        {
            if (source is Product product)
            {
                return $"{_configuration["ApiUrl"]}{product.ImageUrl}";
            }

            if (source is CartItem cartItem)
            {
                return $"{_configuration["ApiUrl"]}{cartItem.Product.ImageUrl}";
            }

            return string.Empty;
        }
    }
}
