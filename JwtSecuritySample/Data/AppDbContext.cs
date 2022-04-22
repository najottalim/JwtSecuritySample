using System;
using JwtSecuritySample.Helpers;
using JwtSecuritySample.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace JwtSecuritySample.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options, IConfiguration config)
        : base(options)
        {
            this.config = config;
        }

        private readonly IConfiguration config;
        public AppDbContext(IConfiguration config)
        {
            this.config = config;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(config.GetConnectionString("AppDb"));
        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserRole> UserRoles { get; set; }
        public virtual DbSet<UserPermission> UserPermissions { get; set; }
        public virtual DbSet<UserRolePermission> UserRolePermissions { get; set; }
    }
}
