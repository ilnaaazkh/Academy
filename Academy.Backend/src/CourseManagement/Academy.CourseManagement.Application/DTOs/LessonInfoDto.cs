namespace Academy.CourseManagement.Application.DTOs
{
    public class LessonInfoDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public int Position { get; set; }
        public string LessonType { get; set; } = default!;
    }
}
