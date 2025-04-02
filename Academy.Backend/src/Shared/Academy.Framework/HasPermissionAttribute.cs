using Microsoft.AspNetCore.Authorization;

namespace Academy.Framework
{
    public class HasPermissionAttribute : AuthorizeAttribute
    {
        public HasPermissionAttribute(string permission)
            : base(policy: permission) { }
    }
}
