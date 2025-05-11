using Academy.CourseManagement.Application.Courses;
using Academy.CourseManagement.Application.Courses.GetLesson;
using Academy.Framework;
using Microsoft.AspNetCore.Mvc;

namespace Academy.CourseManagement.Presentation
{
    public class LessonsController : ApplicationController
    {
        [HttpGet("{lessonId:guid}")]
        public async Task<ActionResult> GetLesson(
            [FromRoute] Guid lessonId,
            [FromServices] GetLessonQueryHandler handler,
            CancellationToken cancellationToken)
        {
            var result = await handler.Handle(new GetLessonQuery(lessonId), cancellationToken);

            return Ok(result.Value);
        }

        [HttpGet("{lessonId:guid}/attachments/{fileUrl}")]
        public async Task<ActionResult> GetAttachmentDownloadLink(
            [FromRoute] Guid lessonId,
            [FromRoute] string fileUrl,
            [FromServices] GetAttachmentDownloadLinkQueryHandler handler,
            CancellationToken cancellationToken)
        {
            var query = new GetAttachmentDownloadLinkQuery(lessonId, fileUrl);
            
            var result = await handler.Handle(query);

            return Ok(result.Value);
        }
    }
}
