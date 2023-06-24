#nullable disable
namespace ExamPapers.Database.ORM.Models;

[Table("answers")]
public class Answer
{
    [Key]
    public int Id { get; set; }
    
    [Required(AllowEmptyStrings = false)]
    public string Text { get; set; }
    
    [Required]
    public bool IsCorrect { get; set; }

    [ForeignKey(nameof(Question))]
    public int QuestionId { get; set; }
    [Required]
    public Question Question { get; set; }
}