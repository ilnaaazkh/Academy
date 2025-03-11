using Academy.CourseManagement.Application.Authors.Create;
using Academy.CourseManagement.Application.Authors.Update.UpdateMainInfo;
using Academy.CourseManagement.Application.Courses.AddLesson;
using Academy.CourseManagement.Application.Courses.AddModule;
using Academy.CourseManagement.Application.Courses.Create;
using Academy.CourseManagement.Application.Courses.Update;
using Academy.CourseManagement.Application.Courses.UpdateModule;
using Academy.CourseManagement.Contracts.Requests;

namespace Academy.CourseManagement.Presentation.Extensions
{
    public static class CommandExtesions
    {
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
    }
}
