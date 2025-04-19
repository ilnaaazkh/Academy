namespace Academy.Accounts.Infrastructure.Models
{
    public class Permission 
    {
        public Permission(string code)
        {
            Code = code;
        }

        public Guid Id { get; }
        public string Code { get; } = string.Empty;
    }
}
