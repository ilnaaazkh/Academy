using Academy.Core.Abstractions;
using Academy.Core.Extensions;
using Academy.Core.Models;
using Academy.FilesService.Contracts;
using Academy.SharedKernel;
using Academy.SharedKernel.ValueObjects.Ids;
using CSharpFunctionalExtensions;

namespace Academy.CourseManagement.Application.Courses.AddCoursePreview
{
    public record AddCoursePreviewCommand(Guid CourseId, IEnumerable<UploadFileCommand> Files, Guid UserId) : ICommand;

    public class AddCoursePreviewCommandHandler : ICommandHandler<AddCoursePreviewCommand>
    {
        private readonly string[] ALLOWED_EXTENSIONS = [".img", ".png", ".jpg", ".jpeg"];
        private readonly string BUCKET = Constants.Buckets.PREVIEW;
        private readonly IFilesServiceContract _filesServiceContract;
        private readonly ICoursesRepository _coursesRepository;

        public AddCoursePreviewCommandHandler(
            IFilesServiceContract filesServiceContract,
            ICoursesRepository coursesRepository)
        {
            _filesServiceContract = filesServiceContract;
            _coursesRepository = coursesRepository;
        }

        public async Task<UnitResult<ErrorList>> Handle(AddCoursePreviewCommand command, CancellationToken cancellationToken = default)
        {
            var courseResult = await _coursesRepository.GetById(CourseId.Create(command.CourseId), cancellationToken);

            if (courseResult.IsFailure)
            {
                return courseResult.Error.ToErrorList();
            }

            if(courseResult.Value.AuthorId != command.UserId)
            {
                return Errors.User.AccessDenied().ToErrorList();
            }

            if(command.Files.Select(f => Path.GetExtension(f.FileName)).Any(ext => !ALLOWED_EXTENSIONS.Contains(ext)))
            {
                return Errors.General.ValueIsInvalid().ToErrorList();
            }

            var filePaths = await _filesServiceContract.UploadFiles(command.Files, BUCKET, cancellationToken);

            if (filePaths.IsFailure)
            {
                return filePaths.Error;
            }

            courseResult.Value.SetPreview(filePaths.Value.First());

            await _coursesRepository.Save(courseResult.Value, cancellationToken);

            return Result.Success<ErrorList>();
        }
    }
}
