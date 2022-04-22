using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace JwtSecuritySample.Models
{
    public class User
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
        public long RoleId { get; set; }
    }
}
