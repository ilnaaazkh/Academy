namespace Academy.Management.Contracts.Requests
{
    public record UpdateAuthoringRequest(
        string Comment,
        string FirstName,
        string LastName,
        string MiddleName);
}
