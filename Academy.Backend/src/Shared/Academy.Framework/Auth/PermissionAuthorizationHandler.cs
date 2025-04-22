using Microsoft.AspNetCore.Authorization;

namespace Academy.Framework.Auth
{
    public class PermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
    {
        private const string Permission = "permission";
        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            PermissionRequirement requirement)
        {
            var claims = context.User.Claims.Where(x => x.Type == Permission);

            if (claims.Any(c => c.Value == requirement.Permission))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
