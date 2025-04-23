using Academy.CourseManagement.Application.Courses.AddLesson;
using Academy.CourseManagement.Application.Courses.AddModule;
using Academy.CourseManagement.Application.Courses.AddPracticeData;
using Academy.CourseManagement.Application.Courses.AddTestToLesson;
using Academy.CourseManagement.Application.Courses.Create;
using Academy.CourseManagement.Application.Courses.GetCourses;
using Academy.CourseManagement.Application.Courses.Update;
using Academy.CourseManagement.Application.Courses.UpdateModule;
using Academy.CourseManagement.Contracts.Requests;

namespace Academy.CourseManagement.Presentation.Extensions
{
    public static class CommandExtesions
    {
        public static GetCoursesQuery ToQuery(this GetCoursesRequest r)
            => new(r.PageSize, r.PageNumber);
        public static AddPracticeDataCommand ToCommand(this AddPracticeDataRequest request, 
            Guid courseId, 
            Guid moduleId, 
            Guid lessonId,
            Guid userId)
        {
            return new AddPracticeDataCommand(
                courseId, moduleId, lessonId, 
                request.TemplateCode, request.Tests.Select(t => new TestDto(t.Input, t.Expected)),
                userId);
        }
    
        public static CreateCourseCommand ToCommand(this CreateCourseRequest request, Guid authorId)
        {
            return new CreateCourseCommand(request.Title, request.Description, authorId);
        }

        public static UpdateCourseCommand ToCommand(this UpdateCourseRequest request, Guid courseId, Guid userId)
        {
            return new UpdateCourseCommand(courseId, request.Title, request.Description, userId);
        }
        
        public static AddModuleCommand ToCommand(this AddModuleRequest request, Guid courseId, Guid userId)
        {
            return new AddModuleCommand(courseId, request.Title, request.Description, userId);
        }
    
        public static UpdateModuleCommand ToCommand(
            this UpdateModuleRequest request, 
            Guid courseId, 
            Guid moduleId, 
            Guid userId)
        {
            return new UpdateModuleCommand(courseId, moduleId, request.Title, request.Description, userId);
        }

        public static AddLessonCommand ToCommand(
            this AddLessonRequest request,
            Guid courseId,
            Guid moduleId,
            Guid userId)
        {
            return new AddLessonCommand(courseId, 
                moduleId, 
                request.Title, 
                request.Content, 
                request.LessonType,
                userId);
        }

        public static AddTestToLessonCommand ToCommand(
            this AddTestToLessonRequest request,
            Guid courseId,
            Guid moduleId,
            Guid lessonId,
            Guid userId
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
                dtos,
                userId);

            return command;
        }
    }
}
