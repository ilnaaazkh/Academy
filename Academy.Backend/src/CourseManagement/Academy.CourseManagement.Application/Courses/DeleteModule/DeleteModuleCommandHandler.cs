using Academy.Core.Abstractions;
using Academy.Core.Extensions;
using Academy.CourseManagement.Application.Courses.Update;
using Academy.SharedKernel;
using Academy.SharedKernel.ValueObjects.Ids;
using CSharpFunctionalExtensions;
using FluentValidation;

namespace Academy.CourseManagement.Application.Courses.DeleteModule
{
    public class DeleteModuleCommandHandler : ICommandHandler<DeleteModuleCommand>
    {
        private readonly ICoursesRepository _coursesRepository;

        public DeleteModuleCommandHandler(ICoursesRepository coursesRepository)
        {
            _coursesRepository = coursesRepository;
        }
        public async Task<UnitResult<ErrorList>> Handle(
            DeleteModuleCommand command, 
            CancellationToken cancellationToken = default)
        {
            var courseId = CourseId.Create(command.CourseId);
            var courseResult = await _coursesRepository.GetById(courseId, cancellationToken);

            if (courseResult.IsFailure)
                return courseResult.Error.ToErrorList();

            var course = courseResult.Value;

            if (course.AuthorId != command.UserId)
                return Errors.User.AccessDenied().ToErrorList();

            var moduleId = ModuleId.Create(command.ModuleId);
            var deleteModuleResult = course.RemoveModule(moduleId);

            if(deleteModuleResult.IsFailure)
                return deleteModuleResult.Error.ToErrorList();

            await _coursesRepository.Save(course, cancellationToken);

            return UnitResult.Success<ErrorList>();
        }
    }
}
