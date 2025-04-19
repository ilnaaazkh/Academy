namespace Academy.Accounts.Infrastructure.Options
{
    public class AdminOptions 
    {
        public const string ADMIN = nameof(ADMIN);

        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
    }
}
