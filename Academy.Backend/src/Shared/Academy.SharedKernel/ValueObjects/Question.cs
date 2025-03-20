using CSharpFunctionalExtensions;
using Newtonsoft.Json;



namespace Academy.SharedKernel.ValueObjects
{
    public class Question : ValueObject
    {
        [JsonConstructor]
        private Question(string title, List<Answer> answers)
        {
            Title = title;
            Answers = answers;
        }

        private Question() { }
        public string Title { get; private set; }
        public IReadOnlyList<Answer> Answers { get; private set; } = new List<Answer>();

        public static Result<Question, Error> Create(string title, List<Answer> answers)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                return Errors.General.ValueIsRequired(nameof(title));
            }

            if (answers is null || answers.Count < 2)
            {
                return Errors.Question.AtLeastTwoAnswersRequired(title);
            }

            if (!answers.Any(a => a.IsCorrect))
            {
                return Errors.Question.AtLeastOneCorrectAnswerRequired(title);
            }

            return new Question(title, answers);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Title;

            foreach (var answer in Answers.Where(a => a.IsCorrect))
            {
                yield return answer;
            }
        }
    }
}
