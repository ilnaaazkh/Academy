using CSharpFunctionalExtensions;

namespace Academy.SharedKernel.ValueObjects
{
    public class Description : ValueObject
    {
        public const int MAX_LENGTH = 2000;
        public string Value { get; }

        private Description() { }
        private Description(string value)
        {
            Value = value;
        }

        public static Result<Description, Error> Create(string description)
        {
            if(string.IsNullOrWhiteSpace(description) || description.Length > MAX_LENGTH)
            {
                return Errors.General.ValueIsInvalid(description);
            }

            return new Description(description);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
