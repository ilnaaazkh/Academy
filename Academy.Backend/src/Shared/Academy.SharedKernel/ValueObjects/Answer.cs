using Academy.SharedKernel;
using CSharpFunctionalExtensions;
using System.Text.Json.Serialization;

namespace Academy.SharedKernel.ValueObjects
{
    public class Answer : ValueObject
    {
        public string Title { get; }
        public bool IsCorrect { get; }

        [JsonConstructor]
        private Answer(string title, bool isCorrect)
        {
            Title = title;
            IsCorrect = isCorrect;
        }
        private Answer() { }

        public static Result<Answer, Error> Create(string title, bool isCorrect)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                return Errors.General.ValueIsRequired(nameof(title));
            }

            return new Answer(title, isCorrect);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Title;
            yield return IsCorrect;
        }
    }
}
