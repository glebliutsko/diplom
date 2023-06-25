using System.ComponentModel.DataAnnotations;

namespace ExamPapers.API.Entity;

public record TestWithQuestionResponse
{
    [Required]
    public required int Id { get; init; }

    [Required(AllowEmptyStrings = false)]
    [StringLength(100)]
    public required string Title { get; init; }
    
    [StringLength(255)]
    public string? Description  { get; init; }
    
    public List<QuestionInTestResponse> Questions { get; init; }
}