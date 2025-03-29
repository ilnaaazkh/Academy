using Academy.CourseManagement.Application.Authors.Create;
using Academy.CourseManagement.Application.Authors.Update.UpdateMainInfo;
using Academy.CourseManagement.Application.Courses.AddLesson;
using Academy.CourseManagement.Application.Courses.AddModule;
using Academy.CourseManagement.Application.Courses.AddPracticeData;
using Academy.CourseManagement.Application.Courses.AddTestToLesson;
using Academy.CourseManagement.Application.Courses.Create;
using Academy.CourseManagement.Application.Courses.Update;
using Academy.CourseManagement.Application.Courses.UpdateModule;
using Academy.CourseManagement.Contracts.Requests;
using System.Linq;

namespace Academy.CourseManagement.Presentation.Extensions
{
    public static class CommandExtesions
    {
        public static AddPracticeDataCommand ToCommand(this AddPracticeDataRequest request, 
            Guid courseId, 
            Guid moduleId, 
            Guid lessonId)
        {
            return new AddPracticeDataCommand(
                courseId, moduleId, lessonId, 
                request.TemplateCode, request.Tests.Select(t => new TestDto(t.Input, t.Expected)));
        }
        public static CreateAuthorCommand ToCommand(this CreateAuthorRequest request) 
        {
            return new CreateAuthorCommand(request.FirstName, request.LastName, request.Email, request.PhoneNumber);
        }

        public static UpdateMainInfoCommand ToCommand(this UpdateAuthorMainInfo request, Guid id)
        {
            return new UpdateMainInfoCommand(
                id,
                request.Email,
                request.PhoneNumber,
                request.FirstName,
                request.LastName
                );
        }
    
        public static CreateCourseCommand ToCommand(this CreateCourseRequest request)
        {
            return new CreateCourseCommand(request.Title, request.Description, request.AuthorId);
        }

        public static UpdateCourseCommand ToCommand(this UpdateCourseRequest request, Guid courseId)
        {
            return new UpdateCourseCommand(courseId, request.Title, request.Description);
        }
        
        public static AddModuleCommand ToCommand(this AddModuleRequest request, Guid courseId)
        {
            return new AddModuleCommand(courseId, request.Title, request.Description);
        }
    
        public static UpdateModuleCommand ToCommand(
            this UpdateModuleRequest request, 
            Guid courseId, 
            Guid moduleId)
        {
            return new UpdateModuleCommand(courseId, moduleId, request.Title, request.Description);
        }

        public static AddLessonCommand ToCommand(
            this AddLessonRequest request,
            Guid courseId,
            Guid moduleId)
        {
            return new AddLessonCommand(courseId, 
                moduleId, 
                request.Title, 
                request.Content, 
                request.LessonType);
        }

        public static AddTestToLessonCommand ToCommand(
            this AddTestToLessonRequest request,
            Guid courseId,
            Guid moduleId,
            Guid lessonId
            )
        {
            var dtos = request.Questions.Select(q =>
                new Application.Courses.AddTestToLesson.TestQuestionDto(
                    q.Title,
                    q.Answers.Select(a =>
                        new Application.Courses.AddTestToLesson.TestAnswerDto(a.Title, a.IsCorrect)
                    ).ToList()
                )
            );

            var command = new AddTestToLessonCommand(
                courseId,
                moduleId,
                lessonId,
                dtos);

            return command;
        }
    }
}
