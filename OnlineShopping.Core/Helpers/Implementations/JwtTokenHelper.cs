using Microsoft.AspNetCore.Identity;

namespace OnlineShopping.Core.Helpers.Implementations
{
    public class JwtTokenHelper : IJwtTokenHelper
    {
        private readonly JwtSettings _jwtSettings;
        private readonly UserManager<AppUser> _userManger;

        public JwtTokenHelper(IOptions<JwtSettings> jwtSettings, UserManager<AppUser> userManager)
        {
            _jwtSettings = jwtSettings.Value;
            _userManger = userManager;
        }
        public async Task<string> GenerateToken(AppUser user)
        {
            var claims = await GetClaims(user);

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_jwtSettings.Key);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(_jwtSettings.ExpiryInMinutes),
                Issuer = _jwtSettings.Issuer,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private async Task<List<Claim>> GetClaims(AppUser user)
        {
            var roles = await _userManger.GetRolesAsync(user);

            var claims = new List<Claim>
            {
                new (ClaimTypes.NameIdentifier, user.Id.ToString()),
                new (ClaimTypes.Email, user.Email),
            };

            foreach(var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            return claims;
        }
    }
}
