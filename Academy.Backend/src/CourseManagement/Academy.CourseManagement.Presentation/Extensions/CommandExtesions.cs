using Academy.CourseManagement.Application.Authors.Create;
using Academy.CourseManagement.Application.Authors.Update.UpdateMainInfo;
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
    }
}
