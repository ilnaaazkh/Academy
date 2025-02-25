using CSharpFunctionalExtensions;

namespace Academy.SharedKernel.ValueObjects
{
    public class Content : ValueObject
    {
        public string Value { get; }

        private Content(string content)
        {
            Value = content;
        }

        public static Result<Content, Error> Create(string content)
        {
            if (string.IsNullOrWhiteSpace(content))
            {
                return Errors.General.ValueIsRequired(content);
            }

            return new Content(content);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
