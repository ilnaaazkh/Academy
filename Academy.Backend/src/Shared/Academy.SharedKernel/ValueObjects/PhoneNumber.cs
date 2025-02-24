using CSharpFunctionalExtensions;
using System.Text.RegularExpressions;

namespace Academy.SharedKernel.ValueObjects
{
    public class PhoneNumber : ValueObject
    {
        private static readonly Regex PhoneRegex = new(@"^\+?\d{10,15}$", RegexOptions.Compiled);

        public string Value { get; }

        private PhoneNumber(string value)
        {
            Value = value;
        }

        public static Result<PhoneNumber, Error> Create(string phoneNumber)
        {
            if (string.IsNullOrWhiteSpace(phoneNumber))
            {
                return Errors.General.ValueIsRequired(nameof(phoneNumber));
            }

            phoneNumber = phoneNumber.Trim();

            if (!PhoneRegex.IsMatch(phoneNumber))
            {
                return Errors.General.ValueIsInvalid(nameof(phoneNumber));
            }

            return new PhoneNumber(phoneNumber);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }

}
