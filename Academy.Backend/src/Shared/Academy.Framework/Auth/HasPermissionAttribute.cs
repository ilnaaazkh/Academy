using Microsoft.AspNetCore.Authorization;

namespace Academy.Framework.Auth
{
    public class HasPermissionAttribute : AuthorizeAttribute
    {
        public HasPermissionAttribute(string permission)
            : base(policy: permission) { }
    }
}
