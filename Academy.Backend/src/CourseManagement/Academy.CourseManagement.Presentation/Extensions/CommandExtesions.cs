using Academy.CourseManagement.Application.Authors.Create;
using Academy.CourseManagement.Contracts.Requests;

namespace Academy.CourseManagement.Presentation.Extensions
{
    public static class CommandExtesions
    {
        public static CreateAuthorCommand ToCommand(this CreateAuthorRequest request) 
        {
            return new CreateAuthorCommand(request.FirstName, request.LastName, request.Email, request.PhoneNumber);
        }
    }
}
