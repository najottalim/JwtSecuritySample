using System;
using JwtSecuritySample.Enums;

namespace JwtSecuritySample.Models
{
    public class User
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
        public UserRole Role { get; set; }
    }
}
