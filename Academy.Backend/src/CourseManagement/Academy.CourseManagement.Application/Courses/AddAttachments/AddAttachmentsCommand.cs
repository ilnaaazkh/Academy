using Academy.Core.Abstractions;
using Academy.Core.Models;

namespace Academy.CourseManagement.Application.Courses.AddAttachments
{
    public record AddAttachmentsCommand(
        Guid CourseId,
        Guid ModuleId,
        Guid LessonId,
        IEnumerable<UploadFileCommand> Files) : ICommand;
}
