using CSharpFunctionalExtensions;

namespace Academy.SharedKernel.ValueObjects
{
    public class FullName : ValueObject
    {
        public const int MAX_LENGTH = 50;

        public string FirstName { get; }
        public string LastName { get; }

        private FullName(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        public static Result<FullName, Error> Create(string firstName, string lastName)
        {
            firstName = firstName.Trim();
            lastName = lastName.Trim();

            if (string.IsNullOrWhiteSpace(firstName))
            {
                return Errors.General.ValueIsRequired(nameof(firstName));
            }

            if (string.IsNullOrWhiteSpace(lastName))
            {
                return Errors.General.ValueIsRequired(nameof(lastName));
            }

            if (firstName.Length > MAX_LENGTH)
            {
                return Errors.General.ValueIsInvalid(nameof(firstName));
            }

            if (lastName.Length > MAX_LENGTH)
            {
                return Errors.General.ValueIsInvalid(nameof(lastName));
            }

            return new FullName(firstName, lastName);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return FirstName;
            yield return LastName;
        }
    }
}
