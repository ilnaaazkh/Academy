using Academy.Core.Abstractions;
using Academy.Management.Application.Authorings.Query.GetAuthoringsQuery;
using CSharpFunctionalExtensions;

namespace Academy.Management.Application.Authorings.Query.GetMyAuthorings
{
    public record GetMyAuthoringsQuery(Guid UserId) : IQuery;

    public class GetMyAuthoringsQueryHandler : IQueryHandler<IReadOnlyList<AuthoringDataModel>, GetMyAuthoringsQuery>
    {
        private readonly IAuthoringsRepository _authoringsRepository;

        public GetMyAuthoringsQueryHandler(IAuthoringsRepository authoringsRepository)
        {
            _authoringsRepository = authoringsRepository;
        }

        public async Task<Result<IReadOnlyList<AuthoringDataModel>>> Handle(GetMyAuthoringsQuery query, CancellationToken cancellationToken)
        {
            var authorings = await _authoringsRepository.GetAuthoringsCreatedByUser(query.UserId, cancellationToken);

            return authorings.Select(a => new AuthoringDataModel(a)).ToList();
        }
    }
}
