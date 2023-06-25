namespace ExamPapers.API.Entity;

public record QuestionInTestResponse
{
    public required QuestionShortResponse Question { get; init; }
    public required int Score { get; init; }
}