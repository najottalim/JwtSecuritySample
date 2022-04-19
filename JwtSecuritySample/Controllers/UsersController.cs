﻿using System;
using System.Collections;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using JwtSecuritySample.Data;
using JwtSecuritySample.Models;
using Microsoft.AspNetCore.Authorization;
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

        [HttpGet, Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await dbContext.Users.ToListAsync());
        }

        [HttpGet("{id}"), Authorize]
        public async Task<IActionResult> Get(long id)
        {
            var user = await dbContext.Users.FirstOrDefaultAsync(p => p.Id == id);

            return Ok(user);
        }
        
        [HttpPost]
        public async Task<IActionResult> Create(User user)
        {
            var entry = dbContext.Users.Add(user);

            await dbContext.SaveChangesAsync();

            return Ok(entry.Entity);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute]long id)
        {
            var exist = await dbContext.Users.FirstOrDefaultAsync(p => p.Id == id);
            if (exist is null)
                return BadRequest(false);

            dbContext.Users.Remove(exist);
            await dbContext.SaveChangesAsync();
                
            return Ok(true);
        }

        [HttpPut]
        public async Task<IActionResult> Update(User user)
        {
            var entry = dbContext.Users.Update(user);

            await dbContext.SaveChangesAsync();

            return Ok(entry.Entity);
        }
        
    }
}