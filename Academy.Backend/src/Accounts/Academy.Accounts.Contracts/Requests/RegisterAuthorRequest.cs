namespace Academy.Accounts.Contracts.Requests
{
    public record RegisterAuthorRequest(
        string Email, 
        string Password,
        string FirstName, 
        string LastName, 
        string MiddleName);
}
