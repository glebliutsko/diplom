using System.ComponentModel.DataAnnotations;

namespace ExamPapers.API.Entity;

public record NewUserRequest
{
    [StringLength(255)]
    public string? FullName { get; init; }
    
    [StringLength(255)]
    public string? Login { get; init; }
    
    [StringLength(30)]
    public string? Role { get; init; }

    public string? Password { get; set; }

    public string? GroupName { get; set; }
    public int? GroupId { get; set; }
}
