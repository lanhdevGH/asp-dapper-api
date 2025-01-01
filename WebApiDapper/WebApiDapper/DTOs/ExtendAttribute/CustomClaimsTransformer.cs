using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

namespace WebApiDapper.DTOs.ExtendAttribute
{
    public class CustomClaimsTransformer : IClaimsTransformation
    {
        public Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
        {
            var identity = (ClaimsIdentity)principal.Identity;

            // Mapping claim tùy chỉnh (nếu cần)
            var rolesClaim = identity.FindFirst("Roles");
            if (rolesClaim != null)
            {
                identity.AddClaim(new Claim(ClaimTypes.Role, rolesClaim.Value));
            }

            return Task.FromResult(principal);
        }
    }
}
