using Academy.SharedKernel;
using CSharpFunctionalExtensions;

namespace Academy.SharedKernel.ValueObjects
{
    public class Question : ValueObject
    {
        private Question(string title, List<Answer> answers)
        {
            Title = title;
            Answers = answers;
        }

        public string Title { get; set; }
        public IReadOnlyList<Answer> Answers { get; }

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
