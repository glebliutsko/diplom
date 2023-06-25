using System.ComponentModel.DataAnnotations;

namespace ExamPapers.API.Entity;

public record QuestionResponse
{
    [Required]
    public required int Id { get; init; }
    
    [Required]
    public required QuestionTypeResponse Type { get; init; }

    [Required(AllowEmptyStrings = false)]
    public required string Text { get; init; }

    public List<AnswerResponse> Answers { get; init; }
}

public record QuestionShortResponse
{
    [Required]
    public required int Id { get; init; }

    [Required(AllowEmptyStrings = false)]
    public required string Text { get; init; }
}