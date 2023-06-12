using System.ComponentModel.DataAnnotations;

namespace ExamPapers.API.Entity;

public record ErrorsListResponse
{
    [Required]
    public required int StatusCode { get; init; }
    
    [Required(AllowEmptyStrings = false)]
    public required string ErrorCode { get; init; }
    
    [Required]
    public required IEnumerable<ErrorResponse> Errors { get; init; }
}

public record ErrorResponse
{
    [Required(AllowEmptyStrings = false)]
    public required string Detail { get; init; }
}