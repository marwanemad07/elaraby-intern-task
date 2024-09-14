namespace OnlineShopping.Core.Helpers.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<AppUser, UserDto>()
                .ForMember(dest => dest.Token, opt => opt.MapFrom<JwtTokenResolver>());

            CreateMap<RegisterDto, AppUser>();

            CreateMap<Product, ProductDto>()
                .ForMember(dest => dest.CategoryName,
                opt => opt.MapFrom(p => p.Category != null ? p.Category.Name : "Category"))
                .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(p => p.CategoryId))
                .ForMember(dest => dest.ImageUrl, 
                    opt => opt.MapFrom<ProductImageUrlResolver<Product, ProductDto>>());
            
            CreateMap<CartItem, CartItemDto>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(p => p.Product.Name))
                .ForMember(dest => dest.ProductId, opt => opt.MapFrom(p => p.Product.Id))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(p => p.Product.Price))
                .ForMember(dest => dest.ImageUrl, 
                    opt => opt.MapFrom<ProductImageUrlResolver<CartItem, CartItemDto>>());

            CreateMap<NewCartItemDto, CartItem>();

            CreateMap<ShoppingCart, ShoppingCartDto>()
                .ForMember(dest => dest.TotalPrice, 
                opt => opt.MapFrom(sc => sc.CartItems.Sum(c => c.Quantity * c.Product.Price)));

            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(p => p.Product.Name));

            CreateMap<Order, OrderDto>()
                .ForMember(dest => dest.TotalPrice, 
                opt => opt.MapFrom(o => o.OrderItems.Sum(oi => oi.Quantity * oi.Price)));

            CreateMap<NewProductDto, Product>()
                .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom<ProductImageFileResolver>());

            CreateMap<Category, CategoryDto>();
        }
    }
}