using System.ComponentModel.DataAnnotations;

namespace ExamPapers.API.Entity;

public record SuccessResponse
{
    [Required]
    public bool Success { get; set; } = true;
}