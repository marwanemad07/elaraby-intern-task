namespace OnlineShopping.Infrastructure.Repositories.Implementations
{

    public class AccountRepository : IAccountRepository
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountRepository(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public async Task<bool> CheckPasswordAsync(string email, string password)
        {
            var user = await GetByEmailAsync(email);
            if (user == null)
            {
                return false;
            }
            var result = await _userManager.CheckPasswordAsync(user, password);
            return result;
        }

        public async Task<IdentityResult> CreateUserAsync(AppUser newUser, string password)
        {
            var result = await _userManager.CreateAsync(newUser, password);
            return result;
        }

        public async Task<AppUser?> GetByEmailAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            return user;
        }

        public async Task<AppUser?> GetByUsernameAsync(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            return user;
        }

        public async Task<SignInResult> LoginAsync(AppUser user, string password)
        {
            var result = await _signInManager.CheckPasswordSignInAsync(user, password, false);
            return result;
        }

        public async Task<IdentityResult> AddToRoleAsync(AppUser user, string role)
        {
            var result = await _userManager.AddToRoleAsync(user, role);
            return result;
        }
    }
}
