using System.ComponentModel.DataAnnotations;

namespace ExamPapers.API.Entity;

public record ErrorsList
{
    [Required]
    public required int StatusCode { get; init; }
    
    [Required(AllowEmptyStrings = false)]
    public required string ErrorCode { get; init; }
    
    [Required]
    public required IEnumerable<Error> Errors { get; init; }
}

public record Error
{
    [Required(AllowEmptyStrings = false)]
    public required string Detail { get; init; }
}