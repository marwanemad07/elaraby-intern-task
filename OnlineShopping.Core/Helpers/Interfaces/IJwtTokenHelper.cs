namespace OnlineShopping.Core.Helpers.Interfaces
{
    public interface IJwtTokenHelper
    {
        public Task<string> GenerateToken(AppUser user);
    }
}
