using CSharpFunctionalExtensions;
using System.Text.RegularExpressions;

namespace Academy.SharedKernel.ValueObjects
{
    public class Email : ValueObject
    {
        private static readonly Regex EmailRegex = new(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.Compiled);

        public string Value { get; }

        private Email(string email)
        {
            Value = email;
        }

        public static Result<Email, Error> Create(string email)
        {
            if(string.IsNullOrWhiteSpace(email))
            {
                return Errors.General.ValueIsRequired(nameof(email));
            }

            if (!EmailRegex.IsMatch(email))
            {
                return Errors.General.ValueIsInvalid(nameof(email));
            }

            return new Email(email);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
