using System.ComponentModel.DataAnnotations;

namespace ExamPapers.API.Entity;

public class GroupResponse
{
    [Required]
    public required int Id { get; init; }

    [Required(AllowEmptyStrings = false)]
    [StringLength(100)]
    public required string Name { get; set; }
}