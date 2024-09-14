namespace OnlineShopping.Infrastructure.Repositories.Interfaces
{
    public interface IAccountRepository
    {
        Task<IdentityResult> CreateUserAsync(AppUser newUser, string password);
        Task<AppUser?> GetByEmailAsync(string email);
        Task<AppUser?> GetByUsernameAsync(string username);
        Task<SignInResult> LoginAsync(AppUser user, string password);
        Task<bool> CheckPasswordAsync(string email, string password);
        Task<IdentityResult> AddToRoleAsync(AppUser user, string role);
    }
}
