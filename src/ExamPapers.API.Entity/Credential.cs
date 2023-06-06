using System.ComponentModel.DataAnnotations;

namespace ExamPapers.API.Entity;

public record Credential
{
    [Required(AllowEmptyStrings = false)]
    public required string Login { get; init; }
    
    [Required(AllowEmptyStrings = false)]
    public required string Password { get; init; }
}