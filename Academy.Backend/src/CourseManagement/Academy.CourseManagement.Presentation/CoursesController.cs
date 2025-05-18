using Academy.CourseManagement.Application.Courses.AddAttachments;
using Academy.CourseManagement.Application.Courses.AddLesson;
using Academy.CourseManagement.Application.Courses.AddModule;
using Academy.CourseManagement.Application.Courses.AddPracticeData;
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
using Academy.Framework.Auth;
using Academy.CourseManagement.Application.Courses.GetCourses;
using Newtonsoft.Json;
using Academy.CourseManagement.Application.Courses.GetCourseModules;
using Academy.CourseManagement.Application.Courses.AddCoursePreview;
using Microsoft.AspNetCore.Authorization;
using Academy.CourseManagement.Application.Courses;
using Academy.CourseManagement.Application.Courses.GetCourse;

namespace Academy.CourseManagement.Presentation
{
    public class CoursesController : ApplicationController
    {
        [HttpGet("{id:guid}")]
        public async Task<ActionResult> GetCourse(
            [FromRoute] Guid id,
            [FromServices] GetCourseQueryHandler handler,
            CancellationToken cancellationToken)
        {
            var result = await handler.Handle(new GetCourseQuery(id), cancellationToken);
            return Ok(result.Value);
        }


        [HttpGet("{courseId:guid}/modules")]
        public async Task<ActionResult> GetCourseModules(
            [FromRoute] Guid courseId,
            [FromServices] GetCourseModulesQueryHandler handler,
            CancellationToken cancellationToken)
        {
            var result = await handler.Handle(new GetCourseModulesQuery(courseId), cancellationToken);

            return Ok(result.Value);
        }

        [HttpGet]
        public async Task<ActionResult> GetCourses(
            [FromQuery] GetCoursesRequest request,
            [FromServices] GetCoursesQueryHandler handler,
            CancellationToken cancellationToken)
        {
            var result = await handler.Handle(request.ToQuery(), cancellationToken);

            var value = result.Value;

            var metadata = new
            {
                value.TotalCount,
                value.PageSize,
                value.CurrentPage,
                value.TotalPages,
                value.HasNext,
                value.HasPrevious
            };

            Response.Headers["X-Pagination"] = JsonConvert.SerializeObject(metadata);

            return Ok(value);
        }

        [HttpPost]
        [HasPermission(Permissions.Courses.Create)]
        public async Task<ActionResult> CreateCourse(
            [FromBody] CreateCourseRequest request,
            [FromServices] CreateCourseCommandHandler handler,
            CancellationToken cancellationToken)
        {
            var result = await handler.Handle(request.ToCommand(UserId), cancellationToken);

            if (result.IsFailure)
            {
                return result.Error.ToResponse();
            }

            return Ok(result.Value);
        }

        [HttpPut("{id:guid}")]
        [HasPermission(Permissions.Courses.Update)]
        public async Task<ActionResult> UpdateCourse(
            [FromRoute] Guid id,
            [FromBody] UpdateCourseRequest request,
            [FromServices] UpdateCourseCommandHandler handler,
            CancellationToken cancellationToken)
        {
            var result = await handler.Handle(request.ToCommand(id, UserId), cancellationToken);

            if (result.IsFailure)
            {
                return result.Error.ToResponse();
            }

            return Ok(result.Value);
        }

        [HttpDelete("{id:guid}")]
        [HasPermission(Permissions.Courses.Delete)]
        public async Task<ActionResult> DeleteCourse(
            [FromRoute] Guid id,
            [FromServices] DeleteCourseCommandHandler handler,
            CancellationToken cancellationToken)
        {
            var result = await handler.Handle(new DeleteCourseCommand(id, UserId), cancellationToken);

            if (result.IsFailure)
            {
                return result.Error.ToResponse();
            }

            return NoContent();
        }

        [HttpPost("{courseId:guid}/modules")]
        [HasPermission(Permissions.Courses.Create)]
        public async Task<ActionResult> AddModule(
            [FromRoute] Guid courseId,
            [FromBody] AddModuleRequest request,
            [FromServices] AddModuleCommandHandler handler,
            CancellationToken cancellationToken)
        {
            var result = await handler.Handle(request.ToCommand(courseId, UserId), cancellationToken);

            if (result.IsFailure)
            {
                return result.Error.ToResponse();
            }

            return Ok(result.Value);
        }

        [HttpPut("{courseId:guid}/modules/{moduleId:guid}")]
        [HasPermission(Permissions.Courses.Update)]
        public async Task<ActionResult> UpdateModule(
            [FromRoute] Guid courseId,
            [FromRoute] Guid moduleId,
            [FromBody] UpdateModuleRequest request,
            [FromServices] UpdateModuleCommandHandler handler,
            CancellationToken cancellationToken
            )
        {
            var result = await handler.Handle(request.ToCommand(courseId, moduleId, UserId), cancellationToken);

            if (result.IsFailure)
            {
                return result.Error.ToResponse();
            }

            return Ok(result.Value);
        }

        [HttpDelete("{courseId:guid}/modules/{moduleId:guid}")]
        [HasPermission(Permissions.Courses.Delete)]
        public async Task<ActionResult> DeleteModule(
            [FromRoute] Guid courseId,
            [FromRoute] Guid moduleId,
            [FromServices] DeleteModuleCommandHandler handler,
            CancellationToken cancellationToken)
        {
            var result = await handler.Handle(new DeleteModuleCommand(courseId, moduleId, UserId), cancellationToken);

            if (result.IsFailure)
            {
                return result.Error.ToResponse();
            }

            return NoContent();
        }

        [HttpPost("{courseId:guid}/modules/{moduleId:guid}/lessons")]
        [HasPermission(Permissions.Courses.Create)]
        public async Task<ActionResult> AddLesson(
            [FromRoute] Guid courseId,
            [FromRoute] Guid moduleId,
            [FromBody] AddLessonRequest request,
            [FromServices] AddLessonCommandHandler handler,
            CancellationToken cancellationToken)
        {
            var result = await handler.Handle(request.ToCommand(courseId, moduleId, UserId), cancellationToken);

            if (result.IsFailure)
            {
                return result.Error.ToResponse();
            }

            return Ok(result.Value);
        }

        [HttpDelete("{courseId:guid}/modules/{moduleId:guid}/lessons/{lessonId:guid}")]
        [HasPermission(Permissions.Courses.Delete)]
        public async Task<ActionResult> RemoveLesson(
            [FromRoute] Guid courseId,
            [FromRoute] Guid moduleId,
            [FromRoute] Guid lessonId,
            [FromServices] DeleteLessonCommandHandler handler, 
            CancellationToken cancellationToken)
        {
            var command = new DeleteLessonCommand(courseId, moduleId, lessonId, UserId);
            var result = await handler.Handle(command, cancellationToken);

            if (result.IsFailure)
            {
                return result.Error.ToResponse();
            }

            return NoContent();
        }

        [HttpPost("{courseId:guid}/modules/{moduleId:guid}/lessons/{lessonId:guid}/test-questions")]
        [HasPermission(Permissions.Courses.Create)]
        public async Task<ActionResult> AddTestToLesson(
            [FromRoute] Guid courseId,
            [FromRoute] Guid moduleId,
            [FromRoute] Guid lessonId,
            [FromBody] AddTestToLessonRequest request,
            [FromServices] AddTestToLessonCommandHandler handler,
            CancellationToken cancellationToken)
        {
            var command = request.ToCommand(courseId, moduleId, lessonId, UserId);

            var result = await handler.Handle(command, cancellationToken);

            if (result.IsFailure)
                return result.Error.ToResponse();

            return Ok(result.Value);
        }

        [HttpPost("{courseId:guid}/modules/{moduleId:guid}/lessons/{lessonId:guid}/attachments")]
        [HasPermission(Permissions.Courses.Create)]
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

            var command = new AddAttachmentsCommand(courseId, moduleId, lessonId, processedFiles, UserId);
            var result = await handler.Handle(command, cancellationToken);

            if (result.IsFailure)
                return result.Error.ToResponse();

            return Ok(result.Value);
        }
        
        
        [HttpPost("{courseId:guid}/modules/{moduleId:guid}/lessons/{lessonId:guid}/practice")]
        [HasPermission(Permissions.Courses.Create)]
        public async Task<ActionResult> AddPracticeDataToLesson(
           [FromRoute] Guid courseId,
           [FromRoute] Guid moduleId,
           [FromRoute] Guid lessonId,
           [FromBody] AddPracticeDataRequest request,
           [FromServices] AddPracticeDataCommandHandler handler,
           CancellationToken cancellationToken)
        {
            var command = request.ToCommand(courseId, moduleId, lessonId, UserId);
            var result = await handler.Handle(command, cancellationToken);

            if (result.IsFailure)
                return result.Error.ToResponse();

            return Ok(result.Value);
        }

        [HttpPost("{courseId:guid}/preview")]
        [HasPermission(Permissions.Courses.Create)]
        public async Task<ActionResult> AddCoursePreview(
            [FromRoute] Guid courseId,
            [FromForm] IFormFileCollection files,
            [FromServices] AddCoursePreviewCommandHandler handler,
            CancellationToken cancellationToken)
        {
            await using var formFileProcessor = new FormFileProcessor();
            var processedFiles = formFileProcessor.Process(files);

            var command = new AddCoursePreviewCommand(courseId, processedFiles, UserId);
            var result = await handler.Handle(command, cancellationToken);

            if (result.IsFailure)
                return result.Error.ToResponse();

            return Ok();
        }

        [HttpGet("my-courses")]
        [Authorize]
        public async Task<ActionResult> GetAuthorCourses(
            [FromServices] GetAuthorCoursesQueryHandler handler,
            CancellationToken cancellationToken)
        {
            var result = await handler.Handle(new GetAuthorCoursesQuery(UserId));

            return Ok(result.Value);
        }
    }
}
