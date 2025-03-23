namespace Academy.CourseManagement.Contracts.Requests
{
    public record AddPracticeDataRequest(
        string TemplateCode,
        IEnumerable<TestCaseDto> Tests);

    public record TestCaseDto(IEnumerable<int> Input, int Expected);
}
