using System.ComponentModel.DataAnnotations;

namespace ExamPapers.API.Entity;

public record TestShortResponse
{
    [Required]
    public required int Id { get; init; }

    [Required(AllowEmptyStrings = false)]
    [StringLength(100)]
    public required string Title { get; init; }
    
    [StringLength(255)]
    public string? Description  { get; init; }

    public int CountQuestion { get; init; }
}