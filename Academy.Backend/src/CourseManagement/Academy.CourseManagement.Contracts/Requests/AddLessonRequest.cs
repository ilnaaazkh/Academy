using System.Security.Cryptography.X509Certificates;

namespace Academy.CourseManagement.Contracts.Requests
{
    public record AddLessonRequest(string Title, string Content, string LessonType);

    public record TestQuestionDto(
        string Title,
        List<TestAnswerDto> Answers
        );

    public record TestAnswerDto(string Title, bool IsCorrect);
}
