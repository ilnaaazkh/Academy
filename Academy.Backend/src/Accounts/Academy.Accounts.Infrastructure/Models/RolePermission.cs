namespace Academy.Accounts.Infrastructure.Models
{
    public class RolePermission 
    {
        public RolePermission(Guid roleId, Guid permissionId)
        {
            RoleId = roleId;
            PermissionId = permissionId;
        }

        public Guid RoleId { get; }
        public Guid PermissionId { get; }

        
    }
}
