using CSharpFunctionalExtensions;
using Newtonsoft.Json;

namespace Academy.SharedKernel.ValueObjects
{
    public class PracticeLessonData : ValueObject
    {
        public string TemplateCode { get; }


        [JsonConstructor]
        private PracticeLessonData(string templateCode)
        {
            TemplateCode = templateCode;
        }

        public static Result<PracticeLessonData, Error> Create(string templateCode)
        {
            if (string.IsNullOrWhiteSpace(templateCode))
            {
                return Errors.General.ValueIsRequired(nameof(templateCode));
            }

            return new PracticeLessonData(templateCode);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return TemplateCode;
        }
    }
}
