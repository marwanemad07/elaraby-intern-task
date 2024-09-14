namespace OnlineShopping.Core.Services.Interfaces
{
    public interface IAccountService
    {
        Task<RestDto<UserDto?>> CreateUserAsync(RegisterDto registerDto);
        Task<RestDto<UserDto?>> LoginAsync(LoginDto loginDto);
    }
}
