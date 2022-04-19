using System;
using JwtSecuritySample.Models;
using Microsoft.EntityFrameworkCore;

namespace JwtSecuritySample.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
        :base(options)
        {
        }
        
        public virtual DbSet<User> Users { get; set; }
    }
}
