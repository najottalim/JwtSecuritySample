using System.ComponentModel.DataAnnotations.Schema;

namespace JwtSecuritySample.Models
{
    public class UserRolePermission
    {
        public long Id { get; set; }
        public long RoleId { get; set; }

        [ForeignKey(nameof(RoleId))]
        public UserRole Role { get; set; }
        
        public long PermissionId { get; set; }
        
        [ForeignKey(nameof(PermissionId))]
        public UserPermission Permission { get; set; }
    }
}
