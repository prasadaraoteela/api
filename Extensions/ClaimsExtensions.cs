using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace api.Extensions
{
    public static class ClaimsExtensions
    {
        public static string GetUserName(this ClaimsPrincipal user)
        {
            return user.Claims.SingleOrDefault(claim => claim.Type.Equals("https://schemas.xmlsoap.org/ws/"))?.Value ?? string.Empty;
        }
    }
}