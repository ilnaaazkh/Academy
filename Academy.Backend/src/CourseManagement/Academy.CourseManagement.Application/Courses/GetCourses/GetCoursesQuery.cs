using Academy.Core.Abstractions;

namespace Academy.CourseManagement.Application.Courses.GetCourses
{
    public record GetCoursesQuery(int PageSize, int PageNumber) : IQuery;
}
