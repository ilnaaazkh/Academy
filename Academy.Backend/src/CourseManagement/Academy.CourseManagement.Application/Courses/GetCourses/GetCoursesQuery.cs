using Academy.Core.Abstractions;
using Academy.Core.Models;
using Academy.CourseManagement.Application.DTOs;
using Academy.CourseManagement.Application.Extensions;
using Academy.CourseManagement.Application.Interfaces;
using Academy.CourseManagement.Domain;
using CSharpFunctionalExtensions;

namespace Academy.CourseManagement.Application.Courses.GetCourses
{
    public record GetCoursesQuery(int PageSize, int PageNumber) : IQuery;

    public class GetCoursesQueryHandler : IQueryHandler<PagedList<CourseDto>, GetCoursesQuery>
    {
        private readonly IReadDbContext _readDbContext;

        public GetCoursesQueryHandler(IReadDbContext readDbContext)
        {
            _readDbContext = readDbContext;
        }

        public async Task<Result<PagedList<CourseDto>>> Handle(GetCoursesQuery query, CancellationToken cancellationToken = default)
        {
            var coursesQuery = _readDbContext.Courses;

            coursesQuery.Where(s => s.Status == Status.Published);

            var result = await coursesQuery.ToPagedList(query.PageSize, query.PageNumber);

            return result;
        }
    }
}
