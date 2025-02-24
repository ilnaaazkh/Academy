using CSharpFunctionalExtensions;

namespace Academy.SharedKernel.ValueObjects.Ids
{
    public class AuthorId : ComparableValueObject
    {
        public Guid Value { get; set; }

        private AuthorId(Guid id)
        {
            Value = id;
        }

        public static AuthorId Create(Guid id) => new AuthorId(id);
        public static AuthorId NewAuthorId() => new AuthorId(Guid.NewGuid());

        protected override IEnumerable<IComparable> GetComparableEqualityComponents()
        {
            yield return Value;
        }
    }
}
