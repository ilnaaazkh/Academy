using Academy.Core.Abstractions;
using Academy.Core.Models;
using Academy.CourseManagement.Application.DTOs;
using Academy.CourseManagement.Application.Extensions;
using Academy.CourseManagement.Application.Interfaces;
using Academy.CourseManagement.Domain;
using Academy.FilesService.Contracts;
using CSharpFunctionalExtensions;

namespace Academy.CourseManagement.Application.Courses.GetCourses
{
    public class GetCoursesQueryHandler : IQueryHandler<PagedList<CourseDto>, GetCoursesQuery>
    {
        private readonly IReadDbContext _readDbContext;
        private readonly IFilesServiceContract _filesServiceContract;

        public GetCoursesQueryHandler(IReadDbContext readDbContext, 
            IFilesServiceContract filesServiceContract)
        {
            _readDbContext = readDbContext;
            _filesServiceContract = filesServiceContract;
        }

        public async Task<Result<PagedList<CourseDto>>> Handle(GetCoursesQuery query, CancellationToken cancellationToken = default)
        {
            var coursesQuery = _readDbContext.Courses;

            //coursesQuery = coursesQuery.Where(s => s.Status == Status.Published.Value);


            var result = await coursesQuery.ToPagedList(query.PageSize, query.PageNumber);

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
