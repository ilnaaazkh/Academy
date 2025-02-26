using Academy.SharedKernel;
using CSharpFunctionalExtensions;


namespace Academy.CourseManagement.Domain
{
    public class Status : ValueObject
    {
        public const int MAX_LENGTH = 20;
        public static Status Draft = new("DRAFT");
        public static Status UnderModeration = new("UNDER_MODERATION");
        public static Status Published = new("PUBLISHED");

        private static readonly List<Status> AllowedStatuses = [Draft, UnderModeration, Published];

        public string Value { get; }

        private Status(string value)
        {
            Value = value;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        public static Result<Status, Error> Create(string status)
        {
            var courseStatus = AllowedStatuses.FirstOrDefault(s => s.Value == status.ToUpper());

            if (courseStatus is null)
            {
                return Errors.General.ValueIsInvalid(nameof(Status));
            }

            return courseStatus;
        }
    }
}
