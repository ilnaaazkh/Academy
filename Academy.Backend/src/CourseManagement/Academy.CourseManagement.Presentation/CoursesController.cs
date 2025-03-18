using Academy.CourseManagement.Application.Courses.AddAttachments;
using Academy.CourseManagement.Application.Courses.AddLesson;
using Academy.CourseManagement.Application.Courses.AddModule;
using Academy.CourseManagement.Application.Courses.AddTestToLesson;
using Academy.CourseManagement.Application.Courses.Create;
using Academy.CourseManagement.Application.Courses.Delete;
using Academy.CourseManagement.Application.Courses.DeleteLesson;
using Academy.CourseManagement.Application.Courses.DeleteModule;
using Academy.CourseManagement.Application.Courses.Update;
using Academy.CourseManagement.Application.Courses.UpdateModule;
using Academy.CourseManagement.Contracts.Requests;
using Academy.CourseManagement.Presentation.Extensions;
using Academy.Framework;
using Academy.Framework.Processors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Academy.CourseManagement.Presentation
{
    public class CoursesController : ApplicationController
    {
        [HttpPost]
        public async Task<ActionResult> CreateCourse(
            [FromBody] CreateCourseRequest request,
            [FromServices] CreateCourseCommandHandler handler,
            CancellationToken cancellationToken)
        {
            var result = await handler.Handle(request.ToCommand(), cancellationToken);

            if (result.IsFailure)
            {
                return result.Error.ToResponse();
            }

            return Ok(result.Value);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult> UpdateCourse(
            [FromRoute] Guid id,
            [FromBody] UpdateCourseRequest request,
            [FromServices] UpdateCourseCommandHandler handler,
            CancellationToken cancellationToken)
        {
            var result = await handler.Handle(request.ToCommand(id), cancellationToken);

            if (result.IsFailure)
            {
                return result.Error.ToResponse();
            }

            return Ok(result.Value);
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> DeleteCourse(
            [FromRoute] Guid id,
            [FromServices] DeleteCourseCommandHandler handler,
            CancellationToken cancellationToken)
        {
            var result = await handler.Handle(new DeleteCourseCommand(id), cancellationToken);

            if (result.IsFailure)
            {
                return result.Error.ToResponse();
            }

            return NoContent();
        }

        [HttpPost("{courseId:guid}/modules")]
        public async Task<ActionResult> AddModule(
            [FromRoute] Guid courseId,
            [FromBody] AddModuleRequest request,
            [FromServices] AddModuleCommandHandler handler,
            CancellationToken cancellationToken)
        {
            var result = await handler.Handle(request.ToCommand(courseId), cancellationToken);

            if (result.IsFailure)
            {
                return result.Error.ToResponse();
            }

            return Ok(result.Value);
        }

        [HttpPut("{courseId:guid}/modules/{moduleId:guid}")]
        public async Task<ActionResult> UpdateModule(
            [FromRoute] Guid courseId,
            [FromRoute] Guid moduleId,
            [FromBody] UpdateModuleRequest request,
            [FromServices] UpdateModuleCommandHandler handler,
            CancellationToken cancellationToken
            )
        {
            var result = await handler.Handle(request.ToCommand(courseId, moduleId), cancellationToken);

            if (result.IsFailure)
            {
                return result.Error.ToResponse();
            }

            return Ok(result.Value);
        }

        [HttpDelete("{courseId:guid}/modules/{moduleId:guid}")]
        public async Task<ActionResult> UpdateModule(
            [FromRoute] Guid courseId,
            [FromRoute] Guid moduleId,
            [FromServices] DeleteModuleCommandHandler handler,
            CancellationToken cancellationToken)
        {
            var result = await handler.Handle(new DeleteModuleCommand(courseId, moduleId), cancellationToken);

            if (result.IsFailure)
            {
                return result.Error.ToResponse();
            }

            return NoContent();
        }

        [HttpPost("{courseId:guid}/modules/{moduleId:guid}/lessons")]
        public async Task<ActionResult> AddLesson(
            [FromRoute] Guid courseId,
            [FromRoute] Guid moduleId,
            [FromBody] AddLessonRequest request,
            [FromServices] AddLessonCommandHandler handler,
            CancellationToken cancellationToken)
        {
            var result = await handler.Handle(request.ToCommand(courseId, moduleId), cancellationToken);

            if (result.IsFailure)
            {
                return result.Error.ToResponse();
            }

            return Ok(result.Value);
        }

        [HttpDelete("{courseId:guid}/modules/{moduleId:guid}/lessons/{lessonId:guid}")]
        public async Task<ActionResult> RemoveLesson(
            [FromRoute] Guid courseId,
            [FromRoute] Guid moduleId,
            [FromRoute] Guid lessonId,
            [FromServices] DeleteLessonCommandHandler handler, 
            CancellationToken cancellationToken)
        {
            var command = new DeleteLessonCommand(courseId, moduleId, lessonId);
            var result = await handler.Handle(command, cancellationToken);

            if (result.IsFailure)
            {
                return result.Error.ToResponse();
            }

            return NoContent();
        }

        [HttpPost("{courseId:guid}/modules/{moduleId:guid}/lessons/{lessonId:guid}/test-questions")]
        public async Task<ActionResult> AddTestToLesson(
            [FromRoute] Guid courseId,
            [FromRoute] Guid moduleId,
            [FromRoute] Guid lessonId,
            [FromBody] AddTestToLessonRequest request,
            [FromServices] AddTestToLessonCommandHandler handler,
            CancellationToken cancellationToken)
        {
            var command = request.ToCommand(courseId, moduleId, lessonId);

            var result = await handler.Handle(command, cancellationToken);

            if (result.IsFailure)
                return result.Error.ToResponse();

            return Ok(result.Value);
        }

        [HttpPost("{courseId:guid}/modules/{moduleId:guid}/lessons/{lessonId:guid}/attachments")]
        public async Task<ActionResult> AddAttachmentsToLesson(
           [FromRoute] Guid courseId,
           [FromRoute] Guid moduleId,
           [FromRoute] Guid lessonId,
           IFormFileCollection files,
           [FromServices] AddAttachmentsCommandHandler handler,
           CancellationToken cancellationToken)
        {
            await using var formFileProcessor = new FormFileProcessor();
            var processedFiles = formFileProcessor.Process(files);

            var command = new AddAttachmentsCommand(courseId, moduleId, lessonId, processedFiles);
            var result = await handler.Handle(command, cancellationToken);

            if (result.IsFailure)
                return result.Error.ToResponse();

            return Ok(result.Value);
        }
    }
}
