using Academy.Core.Abstractions;
using Academy.Core.Extensions;
using Academy.SharedKernel;
using CSharpFunctionalExtensions;

namespace Academy.CourseManagement.Application.Courses.CodeExecution
{
    public class RunCodeCommandHandler : ICommandHandler<string, RunCodeCommand>
    {
        private readonly ICodeRunner _codeRunner;

        public RunCodeCommandHandler(ICodeRunner codeRunner) 
        {
            _codeRunner = codeRunner;
        }

        public async Task<Result<string, ErrorList>> Handle(RunCodeCommand command, CancellationToken cancellationToken = default)
        {
            var result = await _codeRunner.Run(command.Code, cancellationToken);

            if (result.IsFailure)
                return result.Error.ToErrorList();

            return result.Value;
        }
    }
}
