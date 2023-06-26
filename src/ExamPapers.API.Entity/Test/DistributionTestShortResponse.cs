using System.ComponentModel.DataAnnotations;

namespace ExamPapers.API.Entity;

public record DistributionTestShortResponse
{
    public required int DistributionId { get; init; }

    public required int TestId { get; init; }
    
    [Required]
    public required DateTime DistributionDate { get; init; }
    public DateTime? Deadline { get; init; }
    
    public required TestShortResponse Test { get; init; }
}