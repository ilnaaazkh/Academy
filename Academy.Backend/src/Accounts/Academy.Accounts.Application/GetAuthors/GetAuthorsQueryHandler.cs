using Academy.Accounts.Infrastructure.Models;
using Academy.Core.Abstractions;
using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Identity;

namespace Academy.Accounts.Application.GetAuthors
{

    public class GetAuthorsQueryHandler : IQueryHandler<IReadOnlyList<AuthorDto>, GetAuthorsQuery>
    {
        private readonly UserManager<User> _userManager;

        public GetAuthorsQueryHandler(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<Result<IReadOnlyList<AuthorDto>>> Handle(GetAuthorsQuery query, CancellationToken cancellationToken)
        {
           var users = await _userManager.GetUsersInRoleAsync(Roles.AUTHOR);

            return users.Where(u => Contains(u, query.SearchQuery))
                .Select(u => new AuthorDto(
                    u.Id,
                    u.FirstName, 
                    u.LastName, 
                    u.MiddleName, 
                    u.Email!))
                .ToList();
        }

        private bool Contains(User user, string? searchQuery)
        {
            if (string.IsNullOrEmpty(searchQuery)) return true;

            string[] searchFields = [user.Email!, user.FullName];
            return searchFields.Any(f => f.Contains(searchQuery));
        }
    }
}
