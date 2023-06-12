using System.ComponentModel.DataAnnotations;

namespace ExamPapers.API.Entity;

public record TokenResponse
{
    [Required(AllowEmptyStrings = false)]
    [StringLength(70)]
    public required string AccessToken { get; init; }
    
    [Required]
    public required DateTime Expire { get; init; }
    
    public required UserResponse UserResponse { get; init; }
}