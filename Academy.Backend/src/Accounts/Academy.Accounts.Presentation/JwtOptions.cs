namespace Academy.Accounts.Presentation
{
    public class JwtOptions
    {
        public static string JWT = "Jwt";


        public string Issuer { get; init; } = string.Empty;
        public string Audience { get; init; } = string.Empty;
        public int LifetimeInMinutes { get; init; }
    }
}
