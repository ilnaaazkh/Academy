using Academy.SharedKernel.ValueObjects;
using Academy.SharedKernel.ValueObjects.Ids;
using CSharpFunctionalExtensions;

namespace Academy.CourseManagement.Domain
{
    public class Author : Entity<AuthorId>
    {
        public Email Email { get; private set; }
        public FullName FullName { get; private set; }
        public PhoneNumber PhoneNumber { get; private set; }
        public Description? Bio { get; set; }
        public IReadOnlyList<SocialLink> SocialLinks { get; private set; } = new List<SocialLink>();

        public Author(AuthorId id, 
            FullName fullName, 
            Email email, 
            PhoneNumber phoneNumber) : base(id)
        {
            Email = email;
            FullName = fullName;
            PhoneNumber = phoneNumber;
        }

        private Author() { }
    }
}
