using Academy.Core.Abstractions;

namespace Academy.CourseManagement.Application.Courses.GetCoursesUnderModeration
{
    public record GetCoursesUnderModerationQuery(int PageSize, int PageNumber) : IQuery;
}
