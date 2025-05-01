using Academy.SharedKernel;
using CSharpFunctionalExtensions;

namespace Academy.CourseManagement.Application.Courses.CodeExecution
{
    public interface ICodeRunner
    {
        Task<Result<string, Error>> Run(string code, CancellationToken cancellationToken);
    }
}
