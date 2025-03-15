using CSharpFunctionalExtensions;

namespace Academy.SharedKernel.ValueObjects
{
    public class PracticeLessonData : ValueObject
    {
        public string TemplateCode { get; }
        public string SolutionPath { get; }

        private PracticeLessonData(string templateCode, string solutionPath)
        {
            TemplateCode = templateCode;
            SolutionPath = solutionPath;
        }

        public static Result<PracticeLessonData, Error> Create(string templateCode, string solutionPath)
        {
            if (string.IsNullOrWhiteSpace(templateCode))
            {
                return Errors.General.ValueIsRequired(nameof(templateCode));
            }

            if (string.IsNullOrWhiteSpace(solutionPath))
            {
                return Errors.General.ValueIsRequired(nameof(solutionPath));
            }

            return new PracticeLessonData(templateCode, solutionPath);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return TemplateCode;
            yield return SolutionPath;
        }
    }
}
