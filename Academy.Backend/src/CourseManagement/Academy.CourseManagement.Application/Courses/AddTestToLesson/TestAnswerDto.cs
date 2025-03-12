namespace Academy.CourseManagement.Application.Courses.AddTestToLesson
{
    public record TestAnswerDto {
        public string Title { get; }
        public bool IsCorrect { get; } 

        public TestAnswerDto(string title, bool isCorrect)
        {
            Title = title;
            IsCorrect = isCorrect;
        }
    }
}
