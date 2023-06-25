namespace ExamPapers.Database.ORM.Models;

#nullable disable

[Table("questionsInTests")]
[PrimaryKey(nameof(TestId), nameof(QuestionId))]
public class QuestionsInTest
{
    [ForeignKey(nameof(Test))]
    public int TestId { get; set; }
    [Required]
    public Test Test { get; set; }

    [ForeignKey(nameof(Question))]
    public int QuestionId { get; set; }
    [Required]
    public Question Question { get; set; }

    [Required]
    public int Scored { get; set; }
}