using BestAuth.Domain.Entities;

namespace BestAuth.Application.Abstracts
{
    public interface IAuthTokenProcessor
    {
        (string token, DateTime expiresUtc) GenerateAccessToken(User user);

        (string token, DateTime expiresUtc) GenerateRefreshToken();

        void WriteHttpOnlyCookie(string cookieName, string value, DateTime expireUtc);
    }
}
