using Academy.Core.Abstractions;
using Academy.CourseManagement.Application.Interfaces;
using Academy.FilesService.Contracts;
using Academy.SharedKernel;
using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;

namespace Academy.CourseManagement.Application.Courses
{
    public record GetAttachmentDownloadLinkQuery(Guid LessonId, string FileUrl) : IQuery;

    public class GetAttachmentDownloadLinkQueryHandler : IQueryHandler<string, GetAttachmentDownloadLinkQuery>
    {
        private readonly string BUCKET = Constants.Buckets.ATTACHMENTS;

        private readonly IFilesServiceContract _fileServiceContract;
        private readonly IReadDbContext _dbContext;

        public GetAttachmentDownloadLinkQueryHandler(
            IFilesServiceContract fileServiceContract, 
            IReadDbContext dbContext)
        {
            _fileServiceContract = fileServiceContract;
            _dbContext = dbContext;
        }

        public async Task<Result<string>> Handle(GetAttachmentDownloadLinkQuery query, CancellationToken cancellationToken = default)
        {
            var lesson = await _dbContext.Lessons.FirstOrDefaultAsync(l => l.Id == query.LessonId);

            if (lesson == null)
                return "";

            var link = await _fileServiceContract.GetDownloadLink(query.FileUrl, BUCKET, cancellationToken);

            if (link.IsFailure)
                return "";

            return link.Value;
        }
    }
}
