using Academy.SharedKernel;
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
    }
}
