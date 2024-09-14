using OnlineShopping.Infrastructure.Enums;

namespace OnlineShopping.Core.Services.Implementations
{
    public class AccountService : IAccountService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AccountService(IUnitOfWork unitOfWork, IMapper mapper) 
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<RestDto<UserDto?>> CreateUserAsync(RegisterDto registerDto)
        {
            var user = await _unitOfWork.AccountRepository.GetByEmailAsync(registerDto.Email);
            if(user != null)
            {
                return new RestDto<UserDto?>(StatusCodes.Status400BadRequest, null, "User already exists"); 
            }

            var appUser = _mapper.Map<RegisterDto, AppUser>(registerDto);

            var result = await _unitOfWork.AccountRepository.CreateUserAsync(appUser, registerDto.Password);
            if(result.Succeeded)
            {
                var newUser = await _unitOfWork.AccountRepository.GetByEmailAsync(registerDto.Email);
                //TODO: There should be a seperate function to add roles to the user
                await _unitOfWork.AccountRepository.AddToRoleAsync(newUser!, nameof(UserRoles.User));

                UserDto userDto = _mapper.Map<AppUser, UserDto>(newUser!);
                _unitOfWork.Complete();
                var successResponse =  new RestDto<UserDto?>(StatusCodes.Status200OK,
                    userDto, "User created successfully");

                return successResponse;
            }

            var errors = result.Errors.Select(e => e.Description).ToList();
            var badRequesetResponse = new RestDto<UserDto?>(StatusCodes.Status400BadRequest, null,
                "User creation failed", errors);
            return badRequesetResponse;
        }

        public async Task<RestDto<UserDto?>> LoginAsync(LoginDto loginDto)
        {
            var user = await _unitOfWork.AccountRepository.GetByEmailAsync(loginDto.Email);
            if(user == null)
            {
                var notFoundResponse = new RestDto<UserDto?>(StatusCodes.Status404NotFound, null);
                return notFoundResponse;
            }

            var result = await _unitOfWork.AccountRepository.LoginAsync(user, loginDto.Password);
            if (result.Succeeded)
            {
                var userDto = _mapper.Map<AppUser, UserDto>(user!);

                var successResponse = new RestDto<UserDto?>(StatusCodes.Status200OK, userDto);
                return successResponse;
            }

            var unauthorizedResponse = new RestDto<UserDto?>(StatusCodes.Status401Unauthorized, null);
            return unauthorizedResponse;
        }
    }
}
