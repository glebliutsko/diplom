namespace ExamPapers.Database.ORM.Models;

#nullable disable

[Table("questions")]
public class Question
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    public QuestionType Type { get; set; }

    [Required(AllowEmptyStrings = false)]
    public string Text { get; set; }

    public List<Answer> Answers { get; set; }

    [ForeignKey(nameof(Owner))]
    public int OwnerId { get; set; }
    [Required]
    public User Owner { get; set; }

    public List<QuestionsInTest> Tests { get; set; }
}