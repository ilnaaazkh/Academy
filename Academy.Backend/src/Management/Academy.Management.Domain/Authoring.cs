using Academy.SharedKernel;
using Academy.SharedKernel.ValueObjects;
using CSharpFunctionalExtensions;


namespace Academy.Management.Domain
{
    /// <summary>
    /// Запрос на получение роли автора
    /// </summary>
    public class Authoring
    {
        public Guid Id { get; }
        public Guid UserId { get; }
        public string Comment { get; private set; }
        public string? RejectionComment { get; private set; }
        public AuthorRoleRequestStatus Status { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? ReviewedAt { get; private set; }

        public IReadOnlyList<Attachment> Attachments = new List<Attachment>();

        public string FirstName { get; private set; } = string.Empty;
        public string LastName { get; private set; } = string.Empty;
        public string MiddleName { get; private set; } = string.Empty; 

        private Authoring() { }
        public Authoring(Guid id, Guid userId, string comment)
        {
            Id = id;
            UserId = userId;
            Status = AuthorRoleRequestStatus.Draft;
            CreatedAt = DateTime.UtcNow;
            Comment = comment;
        }

        public UnitResult<Error> Approve()
        {
            if (Status != AuthorRoleRequestStatus.Pending)
                return Error.Validation(
                    "authoring.invalid-status",
                    "Only authorings in 'Pending' status can be approved",
                    nameof(Status)
                );

            Status = AuthorRoleRequestStatus.Accepted;
            ReviewedAt = DateTime.UtcNow;
            RejectionComment = null;

            return UnitResult.Success<Error>();
        }

        public UnitResult<Error> Reject(string reason)
        {
            if (Status != AuthorRoleRequestStatus.Pending)
                return Error.Validation(
                    "authoring.invalid-status",
                    "Only authorings in 'Pending' status can be rejected",
                    nameof(Status)
                );

            Status = AuthorRoleRequestStatus.Rejected;
            ReviewedAt = DateTime.UtcNow;
            RejectionComment = reason;

            return UnitResult.Success<Error>();
        }

        public UnitResult<Error> SendOnReview()
        {
            if (Status != AuthorRoleRequestStatus.Draft)
                return Error.Validation(
                    "authoring.invalid-status",
                    "Only authorings in 'Draft' status can be sent for review",
                    nameof(Status)
                );

            Status = AuthorRoleRequestStatus.Pending;
            return UnitResult.Success<Error>();
        }

        public UnitResult<Error> AddAttachments(IEnumerable<Attachment> attachments)
        {
            Attachments = Attachments.Concat(attachments).ToList();

            return UnitResult.Success<Error>();
        }

        public UnitResult<Error> SetComment(string comment)
        {
            Comment = comment;
            return UnitResult.Success<Error>();
        }

        public void SetName(string firstName, string lastName, string middleName)
        {
            (FirstName, LastName, MiddleName) = (firstName, lastName, middleName);
        }
    }
}
