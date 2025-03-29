using CSharpFunctionalExtensions;
using Newtonsoft.Json;

namespace Academy.SharedKernel.ValueObjects
{
    public class PracticeLessonData : ValueObject
    {
        public string TemplateCode { get; }
        public List<Test> Tests { get; }

        [JsonConstructor]
        private PracticeLessonData(string templateCode, List<Test> tests)
        {
            TemplateCode = templateCode;
            Tests = tests;
        }

        public static Result<PracticeLessonData, Error> Create(string templateCode, IEnumerable<Test> tests)
        {
            if (string.IsNullOrWhiteSpace(templateCode))
            {
                return Errors.General.ValueIsRequired(nameof(templateCode));
            }

            return new PracticeLessonData(templateCode, tests.ToList());
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return TemplateCode;

            foreach (var test in Tests)
                yield return test;
        }
    }
}
