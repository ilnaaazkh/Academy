using Academy.SharedKernel;
using CSharpFunctionalExtensions;

namespace Academy.CourseManagement.Domain
{
    public class LessonType : ValueObject
    {
        public static LessonType Lecture = new("LECTURE");
        public static LessonType Practice = new("PRACTICE");
        public static LessonType Test = new("TEST");

        private static readonly List<LessonType> AllowedTypes = [Lecture, Practice, Test];

        public string Value { get; }
        private LessonType(string value)
        {
            Value = value;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        public static Result<LessonType, Error> Create(string type)
        {
            var lessonType = AllowedTypes.FirstOrDefault(t => t.Value == type.ToUpper());

            if (lessonType is null)
            {
                return Errors.General.ValueIsInvalid(nameof(LessonType));
            }

            return lessonType;
        } 
    }
}
