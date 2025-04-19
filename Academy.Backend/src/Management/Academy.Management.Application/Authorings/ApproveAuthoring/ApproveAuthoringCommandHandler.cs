using Academy.Core.Abstractions;
using Academy.Core.Extensions;
using Academy.SharedKernel;
using CSharpFunctionalExtensions;

namespace Academy.Management.Application.Authorings.ApproveAuthoring
{
    public class ApproveAuthoringCommandHandler : ICommandHandler<ApproveAuthoringCommand>
    {
        private readonly IAuthoringsRepository _authoringsRepository;

        public ApproveAuthoringCommandHandler(IAuthoringsRepository authoringsRepository)
        {
            _authoringsRepository = authoringsRepository;
        }

        public async Task<UnitResult<ErrorList>> Handle(ApproveAuthoringCommand command, CancellationToken cancellationToken = default)
        {
            var authoring = await _authoringsRepository.GetById(command.AuthoringId, cancellationToken);

            if (authoring is null)
            {
                return Errors.General.NotFound(command.AuthoringId).ToErrorList();
            }

            var result = authoring.Approve();

            if (result.IsFailure)
                return result.Error.ToErrorList();

            await _authoringsRepository.Save(authoring, cancellationToken);

            return UnitResult.Success<ErrorList>();
        }
    }
}
