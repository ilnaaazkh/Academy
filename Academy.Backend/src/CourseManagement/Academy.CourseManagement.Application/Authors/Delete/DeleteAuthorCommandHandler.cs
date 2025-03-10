using Academy.Core.Abstractions;
using Academy.Core.Extensions;
using Academy.SharedKernel;
using Academy.SharedKernel.ValueObjects.Ids;
using CSharpFunctionalExtensions;

namespace Academy.CourseManagement.Application.Authors.Delete
{
    public class DeleteAuthorCommandHandler : ICommandHandler<Guid, DeleteAuthorCommand>
    {
        private readonly IAuthorsRepository _authorsRepository;

        public DeleteAuthorCommandHandler(IAuthorsRepository authorsRepository)
        {
            _authorsRepository = authorsRepository;
        }

        public async Task<Result<Guid, ErrorList>> Handle(DeleteAuthorCommand command, CancellationToken cancellationToken)
        {
            var authorResult = await _authorsRepository.GetById(AuthorId.Create(command.Id), cancellationToken);

            if (authorResult.IsFailure)
            {
                return authorResult.Error.ToErrorList();
            }

            var deletionResult = await _authorsRepository.Remove(authorResult.Value, cancellationToken);

            if (deletionResult.IsFailure) 
            {
                return deletionResult.Error.ToErrorList();
            }

            return deletionResult.Value;
        }
    }
}
