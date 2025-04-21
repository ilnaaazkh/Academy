using Academy.Core.Extensions;
using Academy.FilesService.Services.Interfaces;
using Academy.FilesService.Services.Models;
using Academy.SharedKernel;
using CSharpFunctionalExtensions;
using Microsoft.Extensions.Options;
using Minio;
using Minio.DataModel.Args;
using Minio.Exceptions;

namespace Academy.FilesService.Infractructure
{
    public class MinioProvider : IFileProvider
    {
        private const int MAX_DEGREE_OF_PARALLELISM = 10;

        private readonly IMinioClient _minioClient;
        private readonly MinioOptions _options;

        public MinioProvider(IMinioClient minioClient, IOptions<MinioOptions> options)
        {
            _minioClient = minioClient;
            _options = options.Value;
        }

        public async Task<Result<IReadOnlyList<string>, ErrorList>> UploadFiles(
            IEnumerable<FileData> files,
            CancellationToken cancellationToken)
        {
            var semaphoreSlim = new SemaphoreSlim(MAX_DEGREE_OF_PARALLELISM);
            var fileList = files.ToList();

            try
            {
                await CreateBucketIfNotExist(fileList, cancellationToken);

                var tasks = fileList.Select(async (file) => await PutObject(file, semaphoreSlim, cancellationToken));

                var pathsResult = await Task.WhenAll(tasks);

                if (pathsResult.Any(r => r.IsFailure))
                {
                    return pathsResult.First(r => r.IsFailure).Error.ToErrorList();
                }
                return pathsResult
                    .Select(r => r.Value)
                    .ToList()
                    .AsReadOnly();
            }
            catch
            {
                return Error.Failure("file.upload", "Fail to upload files in minio").ToErrorList();
            }
        }

        private async Task<Result<string, Error>> PutObject(
            FileData fileData,
            SemaphoreSlim semaphoreSlim,
            CancellationToken cancellationToken)
        {
            await semaphoreSlim.WaitAsync(cancellationToken);

            var path = Guid.NewGuid().ToString() + fileData.Name;
            var putObjectArgs = new PutObjectArgs()
                .WithBucket(fileData.Bucket)
                .WithStreamData(fileData.Content)
                .WithObjectSize(fileData.Content.Length)
                .WithObject(path);

            try
            {
                await _minioClient.PutObjectAsync(putObjectArgs, cancellationToken);
                return path;
            }
            catch
            {
                return Error.Failure("file.upload", "Fail to upload file in minio");
            }
            finally
            {
                semaphoreSlim.Release();
            }
        }

        private async Task CreateBucketIfNotExist(
            List<FileData> fileDatas,
            CancellationToken cancellationToken)
        {
            HashSet<string> bucketNames = [.. fileDatas.Select(f => f.Bucket)];

            foreach (var bucketName in bucketNames)
            {
                var bucketExistArgs = new BucketExistsArgs()
                    .WithBucket(bucketName);

                var isExist = await _minioClient
                    .BucketExistsAsync(bucketExistArgs, cancellationToken);

                if (isExist == false)
                {
                    var makeBucketArgs = new MakeBucketArgs().WithBucket(bucketName);

                    await _minioClient.MakeBucketAsync(makeBucketArgs, cancellationToken);
                }
            }
        }

        public async Task<Result<string, ErrorList>> GetPresignedUrl(
            string fileUrl,
            string bucket,
            CancellationToken cancellationToken)
        {

            try
            {
                PresignedGetObjectArgs args = new PresignedGetObjectArgs()
                    .WithBucket(bucket)
                    .WithObject(fileUrl)
                    .WithExpiry(_options.PresignedUrlExpiryHours * 60 * 60);

                var presigned = await _minioClient.PresignedGetObjectAsync(args);
                return presigned;
            }
            catch (MinioException ex)
            {
                return Errors.General.NotFound(fileUrl).ToErrorList();
            }
        }
    }
}
