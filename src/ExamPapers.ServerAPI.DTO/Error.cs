using System.ComponentModel.DataAnnotations;

namespace ExamPapers.ServerAPI.DTO;

public record Error
{
    [Required]
    public required int StatusCode { get; init; }
    
    [Required(AllowEmptyStrings = false)]
    public required string ErrorCode { get; init; }
    
    [Required(AllowEmptyStrings = false)]
    public required string Detail { get; init; }
}