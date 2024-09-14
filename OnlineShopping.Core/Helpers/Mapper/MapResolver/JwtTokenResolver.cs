namespace OnlineShopping.Core.Helpers.Mapper.MapResolver
{
    public class JwtTokenResolver : IValueResolver<AppUser, UserDto, string>
    {
        private readonly IJwtTokenHelper _jwtTokenHelper;

        public JwtTokenResolver(IJwtTokenHelper jwtTokenHelper)
        {
            _jwtTokenHelper = jwtTokenHelper;
        }
        public string Resolve(AppUser source, UserDto destination, string destMember, ResolutionContext context)
        {
            var generatedToken = _jwtTokenHelper.GenerateToken(source).Result;
            return generatedToken;
        }
    }
}
