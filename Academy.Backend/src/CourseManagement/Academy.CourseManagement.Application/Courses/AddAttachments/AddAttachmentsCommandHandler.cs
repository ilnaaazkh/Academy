using Academy.Core.Abstractions;
using Academy.Core.Extensions;
using Academy.FilesService.Contracts;
using Academy.SharedKernel;
using Academy.SharedKernel.ValueObjects;
using Academy.SharedKernel.ValueObjects.Ids;
using CSharpFunctionalExtensions;

namespace Academy.CourseManagement.Application.Courses.AddAttachments
{
    public class AddAttachmentsCommandHandler : ICommandHandler<Guid, AddAttachmentsCommand>
    {
        private readonly string[] ALLOWED_EXTENSIONS = [".pdf", ".zip"];
        private readonly string BUCKET = Constants.Buckets.ATTACHMENTS;
        private readonly ICoursesRepository _coursesRepository;
        private readonly IFilesServiceContract _filesServiceContract;

        public AddAttachmentsCommandHandler(
            ICoursesRepository coursesRepository,
            IFilesServiceContract filesServiceContract)
        {
            _coursesRepository = coursesRepository;
            _filesServiceContract = filesServiceContract;
        }

        public async Task<Result<Guid, ErrorList>> Handle(
            AddAttachmentsCommand command, 
            CancellationToken cancellationToken = default)
        {
            var courseId = CourseId.Create(command.CourseId);
            var courseResult = await _coursesRepository.GetById(courseId, cancellationToken);

            if (courseResult.IsFailure)
                return courseResult.Error.ToErrorList();

            var course = courseResult.Value;

            if (course.AuthorId != command.UserId)
                return Errors.User.AccessDenied().ToErrorList();

            //Попробовать загрузить файлы в S3 хранилище
            var uploadFilesResult = await _filesServiceContract.UploadFiles(command.Files, BUCKET, cancellationToken);
            if (uploadFilesResult.IsFailure)
                return uploadFilesResult.Error;

            
            var attachmentList = new List<Attachment>();
            foreach(var (file, path) in command.Files.Zip(uploadFilesResult.Value))
            {
                var attachment = Attachment.Create(file.FileName, path);

                if(attachment.IsFailure)
                    return attachment.Error.ToErrorList();

                attachmentList.Add(attachment.Value);
            }

            var moduleId = ModuleId.Create(command.ModuleId);
            var lessonId = LessonId.Create(command.LessonId);

            var result = course.AddAttachmentsToLesson(moduleId, lessonId, attachmentList);

            if(result.IsFailure)
                return result.Error.ToErrorList();

            await _coursesRepository.Save(course, cancellationToken);

            return courseId.Value;
        }
    }
}
