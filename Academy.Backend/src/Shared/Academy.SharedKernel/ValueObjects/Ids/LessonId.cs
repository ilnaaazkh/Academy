using CSharpFunctionalExtensions;

namespace Academy.SharedKernel.ValueObjects.Ids
{
    public class LessonId : ComparableValueObject
    {
        public Guid Value { get; set; }

        private LessonId(Guid id)
        {
            Value = id;
        }

        public static LessonId Create(Guid id) => new LessonId(id);
        public static LessonId NewLessonId() => new LessonId(Guid.NewGuid());

        protected override IEnumerable<IComparable> GetComparableEqualityComponents()
        {
            yield return Value;
        }
    }
}
