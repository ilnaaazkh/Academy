using Academy.Core.Models;
using Academy.FilesService.Contracts;
using Academy.SharedKernel;
using CSharpFunctionalExtensions;

namespace Academy.FilesService.Presentation
{
    public class FilesServiceContract : IFilesServiceContract
    {
        private readonly Services.FilesService _fileService;

        public FilesServiceContract(Services.FilesService fileService)
        {
            _fileService = fileService;
        }
        public Task<Result<IReadOnlyList<string>, ErrorList>> UploadFiles(
            IEnumerable<UploadFileCommand> files, 
            string bucket,
            CancellationToken cancellationToken)
        {
            return _fileService.UploadFiles(files, bucket, cancellationToken);
        }
    }
}
