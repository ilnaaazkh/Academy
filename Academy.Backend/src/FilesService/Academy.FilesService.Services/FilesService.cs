using Academy.Core.Models;
using Academy.FilesService.Services.Interfaces;
using Academy.FilesService.Services.Models;
using Academy.SharedKernel;
using CSharpFunctionalExtensions;

namespace Academy.FilesService.Services
{
    public class FilesService
    {
        private const string BUCKET = "test";
        private readonly IFileProvider _fileProvider;

        public FilesService(IFileProvider fileProvider)
        {
            _fileProvider = fileProvider;
        }

        public async Task<Result<IReadOnlyList<string>, ErrorList>> UploadFiles(
            IEnumerable<UploadFileCommand> files, 
            string bucket,
            CancellationToken cancellationToken)
        {
            var fileDatas = files.Select(f => new FileData(f.FileName, bucket, f.Content));
            return await _fileProvider.UploadFiles(fileDatas, cancellationToken);
        }
    }
}
