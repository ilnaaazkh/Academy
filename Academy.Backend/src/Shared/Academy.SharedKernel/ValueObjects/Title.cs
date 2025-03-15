using CSharpFunctionalExtensions;

namespace Academy.SharedKernel.ValueObjects
{
    public class Title : ValueObject
    {
        public const int MAX_LENGTH = 100;
        public string Value { get; }

        private Title() {}
        private Title(string value)
        {
            Value = value;
        }

        public static Result<Title, Error> Create(string title)
        {
            if(string.IsNullOrWhiteSpace(title) || title.Length > MAX_LENGTH)
            {
                return Errors.General.ValueIsInvalid(nameof(title));
            }

            return new Title(title);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
