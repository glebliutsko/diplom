using System.ComponentModel.DataAnnotations;

namespace ExamPapers.API.Entity;

public record QuestionRequest
{
    [Required]
    public required string Text { get; init; }
    
    [Required]
    public required QuestionTypeResponse Type { get; set; }
    
    public List<AnswerResponse> Answers { get; init; }
}