using Academy.Core.Abstractions;
using Academy.Core.Extensions;
using Academy.SharedKernel;
using Academy.SharedKernel.ValueObjects.Ids;
using CSharpFunctionalExtensions;

namespace Academy.CourseManagement.Application.Courses.DeleteAttachment
{
    public class DeleteAttachmentCommandHandler : ICommandHandler<Guid, DeleteAttachmentCommand>
    {
        private readonly ICoursesRepository _coursesRepository;

        public DeleteAttachmentCommandHandler(ICoursesRepository coursesRepository)
        {
            _coursesRepository = coursesRepository;
        }

        public async Task<Result<Guid, ErrorList>> Handle(
            DeleteAttachmentCommand command, 
            CancellationToken cancellationToken = default)
        {
            var courseId = CourseId.Create(command.CourseId);
            var courseResult = await _coursesRepository.GetById(courseId);

            if (courseResult.IsFailure)
                return courseResult.Error.ToErrorList();

            if (courseResult.Value.AuthorId != command.UserId)
                return Errors.User.AccessDenied().ToErrorList();

            var moduleId = ModuleId.Create(command.ModuleId);
            var lessonId = LessonId.Create(command.LessonId);

            var result = courseResult.Value.RemoveAttachmentFromLesson(moduleId, lessonId, command.fileUrl);

            if (result.IsSuccess)
            {
                //отправить в очередь на удаление
            }

            await _coursesRepository.Save(courseResult.Value, cancellationToken);

            return courseId.Value;
        }
    }
}
