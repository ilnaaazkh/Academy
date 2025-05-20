using Academy.Core.Abstractions;

namespace Academy.CourseManagement.Application.Courses.DeleteAttachment
{
    public record DeleteAttachmentCommand(
        Guid CourseId,
        Guid ModuleId, 
        Guid LessonId, 
        Guid UserId,
        string fileUrl) : ICommand;
}
