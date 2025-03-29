using Academy.Core.Abstractions;

namespace Academy.CourseManagement.Application.Courses.AddPracticeData
{
    public record AddPracticeDataCommand(
        Guid CourseId,
        Guid ModuleId,
        Guid LessonId,
        string TemplateCode,
        IEnumerable<TestDto> Tests) : ICommand;
}
