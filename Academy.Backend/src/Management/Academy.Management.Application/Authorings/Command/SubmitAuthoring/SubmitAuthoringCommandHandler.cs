using Academy.Core.Abstractions;
using Academy.Core.Extensions;
using Academy.SharedKernel;
using CSharpFunctionalExtensions;

namespace Academy.Management.Application.Authorings.Command.SubmitAuthoring
{
    public class SubmitAuthoringCommandHandler : ICommandHandler<SubmitAuthoringCommand>
    {
        private readonly IAuthoringsRepository _authoringsRepository;

        public SubmitAuthoringCommandHandler(IAuthoringsRepository authoringsRepository)
        {
            _authoringsRepository = authoringsRepository;
        }

        public async Task<UnitResult<ErrorList>> Handle(
            SubmitAuthoringCommand command,
            CancellationToken cancellationToken = default)
        {
            var authoring = await _authoringsRepository.GetById(command.AuthoringId, cancellationToken);

            if (authoring is null)
            {
                return Errors.General.NotFound(command.AuthoringId).ToErrorList();
            }

            if (authoring.UserId != command.UserId)
            {
                return Errors.User.AccessDenied().ToErrorList();
            }

            var result = authoring.SendOnReview();

            if (result.IsFailure)
                return result.Error.ToErrorList();

            await _authoringsRepository.Save(authoring, cancellationToken);

            return UnitResult.Success<ErrorList>();
        }
    }
}
