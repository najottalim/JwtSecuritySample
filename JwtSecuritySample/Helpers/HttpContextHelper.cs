using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace JwtSecuritySample.Helpers
{
    public class HttpContextHelper
    {
        public static IHttpContextAccessor Accessor { get; set; }
        public static HttpContext Context => Accessor?.HttpContext;
        public static long? UserId => long.Parse(Context?.User?.FindFirst("Id")?.Value ?? "0");
        public static long? RoleId => long.Parse(Context?.User?.FindFirst(ClaimTypes.Role.ToString())?.Value);
        public static string Route => GetCorrectedRoute();
        public static IConfiguration Configuration;

        private static string GetCorrectedRoute()
        {
            string route = Context?.GetEndpoint().DisplayName;
            IEnumerable<string> parts = route.Split(' ')[0].Split('.').SkipWhile(p => p != "Controllers");

            string result = string.Empty;
            foreach (var part in parts)
            {
                if (part.Contains("Controllers"))
                    continue;

                if (!part.EndsWith("Controller"))
                    result += part.ToLower() + ".";
                else
                    result += part.Remove(part.IndexOf("Controller")).ToLower() + ".";
            }

            return result.Remove(result.Length - 1);
        }
    }
}
