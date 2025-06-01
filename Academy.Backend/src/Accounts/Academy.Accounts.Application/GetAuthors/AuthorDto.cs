namespace Academy.Accounts.Application.GetAuthors
{
    public record AuthorDto(
        Guid Id,
        string FirstName,
        string LastName,
        string MiddleName,
        string Email);
}
