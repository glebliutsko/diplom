using System.ComponentModel.DataAnnotations;

namespace ExamPapers.ServerAPI.DTO;

public record Credential
{
    [Required(AllowEmptyStrings = false)]
    public required string Login { get; init; }
    
    [Required(AllowEmptyStrings = false)]
    public required string Password { get; init; }
}