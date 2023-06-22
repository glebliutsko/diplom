using System.ComponentModel.DataAnnotations;

namespace ExamPapers.API.Entity;

public record NewUserRequest
{
    [Required(AllowEmptyStrings = false)]
    [StringLength(255)]
    public required string FullName { get; init; }
    
    [Required(AllowEmptyStrings = false)]
    [StringLength(255)]
    public required string Login { get; init; }
    
    [Required(AllowEmptyStrings = false)]
    [StringLength(30)]
    public required string Role { get; init; }

    public string? Password { get; set; }
}