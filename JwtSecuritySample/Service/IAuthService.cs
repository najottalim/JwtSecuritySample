using JwtSecuritySample.Models;

namespace JwtSecuritySample.Service
{
    public interface IAuthService
    {
        string CreateToken(string key, string issuer, string expire, User user);
    }
}