using Academy.CourseManagement.Application.DTOs;

namespace Academy.CourseManagement.Application.Interfaces
{
    public interface IReadDbContext
    {
        IQueryable<CourseDto> Courses { get; }
    }
}
