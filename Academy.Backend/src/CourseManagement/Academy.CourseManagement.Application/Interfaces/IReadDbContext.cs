using Academy.CourseManagement.Application.DTOs;

namespace Academy.CourseManagement.Application.Interfaces
{
    public interface IReadDbContext
    {
        IQueryable<CourseDto> Courses { get; }

        IQueryable<ModuleDto> Modules { get; }

        IQueryable<LessonDto> Lessons { get; }
    }
}
