using Academy.Core.Abstractions;

namespace Academy.Accounts.Application.RefreshTokens
{
    public record RefreshTokenCommand(Guid RefreshToken) : ICommand;
}
