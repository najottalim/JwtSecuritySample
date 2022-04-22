using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using JwtSecuritySample.Models;
using Microsoft.IdentityModel.Tokens;

namespace JwtSecuritySample.Service
{
    public class AuthService : IAuthService
    {
        public string CreateToken(string key, string issuer, string expire, User user)
        {
            var claims = new Claim[]
            {
                new Claim(ClaimTypes.Name, user.Phone),
                new Claim("Id", user.Id.ToString()),
                new Claim(ClaimTypes.Role, user.RoleId.ToString())
            };
            
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
            var tokenDescriptor = new JwtSecurityToken(issuer, issuer, claims,
                expires: DateTime.Now.AddMinutes(double.Parse(expire)),
                signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
        }
    }
}