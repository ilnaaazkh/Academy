using Academy.Core.Abstractions;
using Academy.Core.Extensions;
using Academy.SharedKernel;
using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academy.Management.Application.Authorings.Command.RejectAuthoring
{
    public class RejectAuthoringCommandHandler : ICommandHandler<RejectAuthoringCommand>
    {
        private readonly IAuthoringsRepository _authoringsRepository;

        public RejectAuthoringCommandHandler(IAuthoringsRepository authoringsRepository)
        {
            _authoringsRepository = authoringsRepository;
        }

        public async Task<UnitResult<ErrorList>> Handle(RejectAuthoringCommand command, CancellationToken cancellationToken = default)
        {
            var authoring = await _authoringsRepository.GetById(command.AuthoringId, cancellationToken);

            if (authoring is null)
            {
                return Errors.General.NotFound(command.AuthoringId).ToErrorList();
            }

            var result = authoring.Reject(command.Reason);

            if (result.IsFailure)
                return result.Error.ToErrorList();

            await _authoringsRepository.Save(authoring, cancellationToken);

            return UnitResult.Success<ErrorList>();
        }
    }
}
