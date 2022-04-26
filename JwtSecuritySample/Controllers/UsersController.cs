using System;
using System.Collections;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using JwtSecuritySample.Attributes;
using JwtSecuritySample.Data;
using JwtSecuritySample.Helpers;
using JwtSecuritySample.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JwtSecuritySample.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly AppDbContext dbContext;
        public UsersController(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet, HeyAuthorize]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await dbContext.Users.ToListAsync());
        }

        [HttpGet("{id}"), HeyAuthorize]
        public async Task<IActionResult> Get(long id)
        {
            var user = await dbContext.Users.FirstOrDefaultAsync(p => p.Id == id);

            return Ok(user);
        }
        
        [HttpPost, HeyAuthorize]
        public async Task<IActionResult> Create(User user)
        {
            var entry = dbContext.Users.Add(user);

            await dbContext.SaveChangesAsync();

            return Ok(entry.Entity);
        }

        [HttpDelete("{id}"), HeyAuthorize]
        public async Task<IActionResult> Delete([FromRoute]long id)
        {
            var exist = await dbContext.Users.FirstOrDefaultAsync(p => p.Id == id);
            if (exist is null)
                return BadRequest(false);

            dbContext.Users.Remove(exist);
            await dbContext.SaveChangesAsync();
                
            return Ok(true);
        }

        [HttpPut, HeyAuthorize]
        public async Task<IActionResult> Update(User user)
        {
            var entry = dbContext.Users.Update(user);

            await dbContext.SaveChangesAsync();

            return Ok(entry.Entity);
        }
        
        [HttpGet("info")]
        public async Task<IActionResult> GetInfo()
        {
            var user = await dbContext.Users.FirstOrDefaultAsync(p => p.Id.ToString() == HttpContextHelper.UserId.ToString());

            return Ok(user);
        }
    }
}
