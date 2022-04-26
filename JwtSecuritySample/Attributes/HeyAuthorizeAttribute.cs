using JwtSecuritySample.Data;
using JwtSecuritySample.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace JwtSecuritySample.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class HeyAuthorizeAttribute : ActionFilterAttribute
    {
        public override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if(HttpContextHelper.UserId is null)
            {
                context.Result = new UnauthorizedResult();
                return base.OnActionExecutionAsync(context, next);
            }
            
            // logic
            var dbContext = new AppDbContext(HttpContextHelper.Configuration);
            var user = dbContext.Users.FirstOrDefault(p => p.Id == HttpContextHelper.UserId);

            if(user is null)
            {
                context.Result = new UnauthorizedResult();
                return base.OnActionExecutionAsync(context, next);
            }

            if (user.RoleId != HttpContextHelper.RoleId)
            {
                context.Result = new ForbidResult();
            }

            var rolePermissions = dbContext.UserRolePermissions.Where(p => p.RoleId == user.RoleId);

            var permissions = (from rolePermission in rolePermissions
                        join permission in dbContext.UserPermissions on rolePermission.PermissionId equals permission.Id
                        select permission.Name).ToList();

            if(!permissions.Contains(HttpContextHelper.Route))
            {
                context.Result = new ForbidResult();
            }

            return base.OnActionExecutionAsync(context, next);
        }
    }
}
