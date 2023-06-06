using System.ComponentModel.DataAnnotations;

namespace ExamPapers.API.Entity;

public record User
{
    [Required]
    public required int Id { get; init; }
    
    [Required(AllowEmptyStrings = false)]
    [StringLength(255)]
    public required string Login { get; init; }
}