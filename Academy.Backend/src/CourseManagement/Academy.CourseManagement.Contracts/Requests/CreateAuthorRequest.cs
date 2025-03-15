namespace Academy.CourseManagement.Contracts.Requests
{
    public record CreateAuthorRequest
    (
        string FirstName ,
        string LastName ,        
        string Email ,
        string PhoneNumber 
    );
}
