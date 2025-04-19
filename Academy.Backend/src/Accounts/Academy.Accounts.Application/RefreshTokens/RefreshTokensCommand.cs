using Academy.Core.Abstractions;

namespace Academy.Accounts.Application.RefreshTokens
{
    public record RefreshTokensCommand(string AccessToken, Guid RefreshToken) : ICommand;
}
