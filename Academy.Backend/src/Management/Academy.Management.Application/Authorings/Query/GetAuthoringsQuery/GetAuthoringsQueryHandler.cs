using Academy.Core.Abstractions;
using Academy.Core.Models;
using CSharpFunctionalExtensions;
using System.Security.Cryptography.X509Certificates;

namespace Academy.Management.Application.Authorings.Query.GetAuthoringsQuery
{
    public class GetAuthoringsQueryHandler : IQueryHandler<PagedList<AuthoringDataModel>, GetAuthoringsQuery>
    {
        private readonly IAuthoringsRepository _authoringsRepository;

        public GetAuthoringsQueryHandler(IAuthoringsRepository authoringsRepository)
        {
            _authoringsRepository = authoringsRepository;
        }

        public async Task<Result<PagedList<AuthoringDataModel>>> Handle(
            GetAuthoringsQuery query, 
            CancellationToken cancellationToken = default)
        {
            var (authorings, totalCount) = await _authoringsRepository.GetAuthoringsOnPending(query, cancellationToken);

            List<AuthoringDataModel> items = authorings.Select(a => new AuthoringDataModel(a)).ToList();
            var result = new PagedList<AuthoringDataModel>(items, totalCount, query.PageNumber, query.PageSize);

            return result;
        }
    }
}
