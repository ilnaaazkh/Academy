namespace Academy.CourseManagement.Contracts.Requests
{
    public record UpdateAuthorMainInfo(
        string Email,
        string PhoneNumber,
        string FirstName,
        string LastName
        );
}
