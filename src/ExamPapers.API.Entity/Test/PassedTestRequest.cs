namespace ExamPapers.API.Entity;

public record PassedTestRequest
{
    public required int Score { get; set; }
};