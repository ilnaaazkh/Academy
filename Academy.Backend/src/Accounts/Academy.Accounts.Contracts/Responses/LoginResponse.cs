namespace Academy.Accounts.Contracts.Responses
{
    public record LoginResponse(
        string AccessToken, 
        Guid RefreshToken,
        IEnumerable<string?> Roles);
}
