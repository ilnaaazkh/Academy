using Academy.Core.Abstractions;
using Academy.Core.Extensions;
using Academy.CourseManagement.Application.DTOs;
using Academy.CourseManagement.Application.Interfaces;
using Academy.FilesService.Contracts;
using Academy.SharedKernel;
using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;

namespace Academy.CourseManagement.Application.Courses.GetCourse
{
    public record GetCourseQuery(Guid CourseId) : IQuery;

    public class GetCourseQueryHandler : IQueryHandler<CourseDto?, GetCourseQuery>
    {
        private readonly string BUCKET = Constants.Buckets.PREVIEW; 
        private readonly IReadDbContext _dbContext;
        private readonly IFilesServiceContract _filesServiceContract;

        public GetCourseQueryHandler(IReadDbContext dbContext, IFilesServiceContract filesServiceContract)
        {
            _dbContext = dbContext;
            _filesServiceContract = filesServiceContract;
        }

        public async Task<Result<CourseDto?>> Handle(GetCourseQuery query, CancellationToken cancellationToken = default)
        {
            var course = await _dbContext.Courses.FirstOrDefaultAsync(c => c.Id == query.CourseId, cancellationToken);

            if (course is null)
                return course;

            var link = await _filesServiceContract.GetDownloadLink(course.Preview, BUCKET, cancellationToken);

            if (link.IsSuccess)
                course.Preview = link.Value;

            return course;
        }
    }
}
