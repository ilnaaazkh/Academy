namespace Academy.CourseManagement.Application.DTOs
{
    public class ModuleDto 
    {
        public Guid Id { get; set; }
        public Guid CourseId { get; set; }
        public string Title { get; set; } = string.Empty;
        public int Position { get; set; }
        public List<LessonDto> Lessons { get; set; } = new();
    }
}
