using Academy.SharedKernel.ValueObjects;

namespace Academy.CourseManagement.Application.DTOs
{
    public class LessonDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public int Position { get; set; }
        public string? Content { get; set; } = string.Empty;
        public string LessonType { get; set; } = string.Empty;
        public IReadOnlyList<Question> Questions { get; private set; } = new List<Question>();
        public IReadOnlyList<Attachment> Attachments { get; private set; } = new List<Attachment>();
        public PracticeLessonData? PracticeLessonData { get; private set; }
        public LessonInfoDto? LessonInfoDto { get; private set; }
    }
}
