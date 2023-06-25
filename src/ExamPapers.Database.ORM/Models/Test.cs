namespace ExamPapers.Database.ORM.Models;

#nullable disable

[Table("tests")]
public class Test
{
    [Key]
    public int Id { get; set; }

    [Required(AllowEmptyStrings = false)]
    [StringLength(100)]
    public string Title { get; set; }

    [StringLength(255)]
    public string Description  { get; set; }
    
    [ForeignKey(nameof(Owner))]
    public int OwnerId { get; set; }
    [Required]
    public User Owner { get; set; }
    
    public List<QuestionsInTest> QuestionsInTests { get; set; }
}