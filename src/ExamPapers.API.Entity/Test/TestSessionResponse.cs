using System.ComponentModel.DataAnnotations;

namespace ExamPapers.API.Entity;

public record TestSessionResponse
{
    [Required]
    public required int Id { get; init; }

    [Required]
    public required DateTime DistributionDate { get; init; }
    public DateTime? Deadline { get; init; }
    
    [Required]
    public required TestShortResponse Test { get; init; }

    public List<DistributionResponse> Distributions { get; set; }
}

public record DistributionResponse
{
    [Required]
    public required int Id { get; init; }

    [Required]
    public required UserResponse Student { get; set; }

    public TestingResultResponse? Result { get; set; }
}

public record TestingResultResponse
{
    [Required]
    public required int Score { get; set; }

    [Required]
    public required int AllScore { get; set; }
    
    [Required]
    public required DateTime PassedTime { get; set; }
}