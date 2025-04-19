namespace Academy.Accounts.Infrastructure.Options
{

    public class JwtOptions
    {
        public static string JWT = nameof(JWT);


        public string Issuer { get; init; } = string.Empty;
        public string Audience { get; init; } = string.Empty;
        public string Key { get; init; } = string.Empty;
        public int LifetimeInMinutes { get; init; }
    }
}
