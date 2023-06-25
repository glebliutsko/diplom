namespace ExamPapers.API.Entity;

public record TestSessionRequest
{
    public DateTime? Deadline { get; set; }
};