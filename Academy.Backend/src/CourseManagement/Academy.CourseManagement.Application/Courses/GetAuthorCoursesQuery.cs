using Academy.Core.Abstractions;
using Academy.CourseManagement.Application.DTOs;
using Academy.CourseManagement.Application.Interfaces;
using Academy.FilesService.Contracts;
using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;

namespace Academy.CourseManagement.Application.Courses
{
    public record GetAuthorCoursesQuery(Guid AuhtorId) : IQuery;

    public class GetAuthorCoursesQueryHandler : IQueryHandler<IReadOnlyList<CourseDto>, GetAuthorCoursesQuery>
    {
        private readonly IFilesServiceContract _filesServiceContract;
        private readonly IReadDbContext _readDbContext;

        public GetAuthorCoursesQueryHandler(
            IFilesServiceContract filesServiceContract,
            IReadDbContext readDbContext)
        {
            _filesServiceContract = filesServiceContract;
            _readDbContext = readDbContext;
        }

        public async Task<Result<IReadOnlyList<CourseDto>>> Handle(GetAuthorCoursesQuery query, CancellationToken cancellationToken = default)
        {
            var result = await _readDbContext.Courses
                .Where(c => c.AuthorId == query.AuhtorId)
                .ToListAsync(cancellationToken);

            foreach (var course in result)
            {
                if (course.Preview == null) continue;
                var link = await _filesServiceContract.GetDownloadLink(course.Preview, Constants.Buckets.PREVIEW, cancellationToken);
                course.Preview = link.Value;
            }

            return result;
        }
    }
}
