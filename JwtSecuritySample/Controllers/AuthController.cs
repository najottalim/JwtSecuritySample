using System.Threading.Tasks;
using JwtSecuritySample.Data;
using JwtSecuritySample.Service;
using JwtSecuritySample.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace JwtSecuritySample.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService authService;
        private readonly AppDbContext dbContext;
        private readonly IConfiguration config;

        public AuthController(IAuthService authService, AppDbContext dbContext, IConfiguration config)
        {
            this.authService = authService;
            this.dbContext = dbContext;
            this.config = config;
        }
        
        [HttpPost("login")]
        public async Task<IActionResult> CreateToken(LoginViewModel model)
        {
            var user = await dbContext.Users.FirstOrDefaultAsync(p => p.Phone == model.Login && p.Password == model.Password);
            if (user is null)
            {
                return BadRequest("login or password incorrect");
            }

            string key = config.GetSection("Jwt:Key").Value;
            string issuer = config.GetSection("Jwt:Issuer").Value;
            string expire = config.GetSection("Jwt:Expire").Value;
            string token = authService.CreateToken(key, issuer,expire, user);

            return Ok(token);
        }
    }
}