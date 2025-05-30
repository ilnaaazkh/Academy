using Academy.Management.Domain;
using Academy.SharedKernel.ValueObjects;
using System.Security.Cryptography.X509Certificates;

namespace Academy.Management.Application.Authorings.Query.GetAuthoringsQuery
{
    public class AuthoringDataModel
    {
        public Guid Id { get; set; }
        public string? Comment { get; set; }
        public string? RejectionComment { get; set; }

        public string Status { get; set; }

        public IReadOnlyList<Attachment> Attachments = new List<Attachment>();

        public DateTime? CreatedAt { get; set; }

        public AuthoringDataModel(Authoring authoring)
        {
            Id = authoring.Id;
            Comment = authoring.Comment;
            RejectionComment = authoring.RejectionComment;
            Attachments = authoring.Attachments;
            Status = authoring.Status.ToString();
            CreatedAt = authoring.CreatedAt;
        }
    }
}
