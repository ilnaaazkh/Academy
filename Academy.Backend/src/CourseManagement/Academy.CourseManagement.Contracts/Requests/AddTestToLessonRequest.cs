namespace Academy.CourseManagement.Contracts.Requests
{
    public record AddTestToLessonRequest(List<TestQuestionDto> Questions);
}
