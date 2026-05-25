using BestAuth.Application.Abstracts;
using BestAuth.Domain.Entities;
using BestAuth.Infrastructure.Options;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace BestAuth.Infrastructure.Processors
{
    public class AuthTokenProcessor(IOptions<JwtOptions> options, IHttpContextAccessor httpAccessor) : IAuthTokenProcessor
    {
        private readonly JwtOptions _jwtOptions = options.Value;
        private readonly IHttpContextAccessor _httpAccessor = httpAccessor;

        public (string token, DateTime expiresUtc) GenerateAccessToken(User user)
        {
            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Key));
            var creds = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email), // TODO
                new Claim(ClaimTypes.NameIdentifier, user.Name),
            };

            var expires = DateTime.UtcNow.AddMinutes(_jwtOptions.AccessExpireMinutes);

            var jwt = new JwtSecurityToken(issuer: _jwtOptions.Issuer,
                audience: _jwtOptions.Audience,
                claims: claims, expires: expires,
                signingCredentials: creds);

            var token = new JwtSecurityTokenHandler().WriteToken(jwt);

            return (token, expires);
        }


        public (string token, DateTime expiresUtc) GenerateRefreshToken()
        {
            var randomNumber = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            var token = Convert.ToBase64String(randomNumber);

            var expire = DateTime.UtcNow.AddDays(_jwtOptions.RefreshExpireDays);
            var refresh = (token, expire);
            return refresh;
        }

        public void WriteHttpOnlyCookie(string cookieName, string value, DateTime expireUtc)
        {
            var options = new CookieOptions
            {
                Expires = expireUtc,
                HttpOnly = true,
                IsEssential = true,
                Secure = true,
                SameSite = SameSiteMode.Strict //TODO
            };
            _httpAccessor.HttpContext.Response.Cookies.Append(cookieName, value, options);
        }
    }
}
