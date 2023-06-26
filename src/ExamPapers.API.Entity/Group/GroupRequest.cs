using System.ComponentModel.DataAnnotations;

namespace ExamPapers.API.Entity;

public record GroupRequest
{
    [Required(AllowEmptyStrings = false)]
    public required string Name { get; init; }
}