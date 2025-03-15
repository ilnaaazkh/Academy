namespace Academy.CourseManagement.Application.Courses.AddTestToLesson
{
    public record TestQuestionDto
    {
        public string Title { get; }
        public IReadOnlyList<TestAnswerDto> Answers { get; }

        public TestQuestionDto(string title, 
            IReadOnlyList<TestAnswerDto> answers)
        {
            Title = title;
            Answers = answers;
        }
    }
}
