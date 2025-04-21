using Academy.FilesService.Services.Models;
using Academy.SharedKernel;
using CSharpFunctionalExtensions;

namespace Academy.FilesService.Services.Interfaces
{
    public interface IFileProvider
    {
        Task<Result<string, ErrorList>> GetPresignedUrl(string fileUrl, string bucket, CancellationToken cancellationToken);
        Task<Result<IReadOnlyList<string>, ErrorList>> UploadFiles(IEnumerable<FileData> files, CancellationToken cancellationToken);
    }
}
