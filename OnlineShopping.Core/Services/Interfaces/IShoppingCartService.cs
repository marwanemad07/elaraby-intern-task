namespace OnlineShopping.Core.Services.Interfaces
{
    public interface IShoppingCartService
    {
        Task<RestDto<ShoppingCartDto?>> GetActiveCartAsync(string userId);
        Task<RestDto<ShoppingCartDto?>> AddCartItemAsync(NewCartItemDto newCartDto, string userId);
        Task<RestDto<ShoppingCartDto?>> RemoveCartItemAsync(int cartItemId, string userId);
    }
}
