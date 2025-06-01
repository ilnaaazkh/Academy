using Academy.Core.Abstractions;
using Academy.Core.Extensions;
using Academy.Management.Application.Authorings.Query.GetAuthoringsQuery;
using Academy.SharedKernel;
using CSharpFunctionalExtensions;

namespace Academy.Management.Application.Authorings.Query.GetAuthoringQuery
{
    public record GetAuthoringQuery(Guid AuthoringId) : IQuery;

    public class GetAuthoringQueryHandler : IQueryHandler<AuthoringDataModel, GetAuthoringQuery>
    {
        private readonly IAuthoringsRepository _authoringsRepository;

        public GetAuthoringQueryHandler(IAuthoringsRepository authoringsRepository)
        {
            _authoringsRepository = authoringsRepository;
        }

        public async Task<Result<AuthoringDataModel>> Handle(GetAuthoringQuery query, CancellationToken cancellationToken = default)
        {
            var result = await _authoringsRepository.GetById(query.AuthoringId, cancellationToken);

            return result == null ? Result.Failure<AuthoringDataModel>("Not found") : new AuthoringDataModel(result);
        }
    }
}
