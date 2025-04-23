using Academy.Core.Abstractions;
using Academy.Core.Extensions;
using Academy.SharedKernel;

namespace Academy.CourseManagement.Application.Courses.GetLesson
{
    public record GetLessonQuery(Guid LessonId) : IQuery;
}
