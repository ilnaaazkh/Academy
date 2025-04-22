using Academy.Core.Models;
using Academy.SharedKernel;
using CSharpFunctionalExtensions;
using System.Runtime.CompilerServices;

namespace Academy.FilesService.Contracts
{
    public interface IFilesServiceContract
    {
        Task<Result<IReadOnlyList<string>, ErrorList>> UploadFiles(
            IEnumerable<UploadFileCommand> files, 
            string bucket,
            CancellationToken cancellationToken);

        Task<Result<string, ErrorList>> GetDownloadLink(
            string fileUrl, 
            string bucket, 
            CancellationToken cancellationToken);
    }
}
