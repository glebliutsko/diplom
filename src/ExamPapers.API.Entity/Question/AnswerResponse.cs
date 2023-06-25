using System.ComponentModel.DataAnnotations;

namespace ExamPapers.API.Entity;

public record AnswerResponse
{
    [Required(AllowEmptyStrings = false)]
    public required string Text { get; init; }
    
    [Required]
    public required bool IsCorrect { get; init; }
}