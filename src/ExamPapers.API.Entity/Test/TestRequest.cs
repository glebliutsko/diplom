namespace ExamPapers.API.Entity;

public record TestRequest
{
    public required string Title { get; init; }
    public string? Description { get; init; }
    public List<QuestionInTestRequest> Questions { get; init; }
}

public record QuestionInTestRequest
{
    public required int QuestionId { get; init; }
    public required int Score { get; init; }
}