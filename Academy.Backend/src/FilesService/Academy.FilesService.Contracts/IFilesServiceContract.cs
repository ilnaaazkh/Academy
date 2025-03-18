using Academy.Core.Models;
using Academy.SharedKernel;
using CSharpFunctionalExtensions;

namespace Academy.FilesService.Contracts
{
    public interface IFilesServiceContract
    {
        Task<Result<IReadOnlyList<string>, ErrorList>> UploadFiles(
            IEnumerable<UploadFileCommand> files, 
            string bucket,
            CancellationToken cancellationToken);
    }
}
