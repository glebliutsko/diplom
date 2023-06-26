namespace ExamPapers.Database.ORM.Models;

#nullable disable

[Table("testingResults")]
public class TestingResult
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int Score { get; set; }

    [Required]
    public int AllScore { get; set; }

    [ForeignKey(nameof(DistributionTest))]
    public int DistributionTestId { get; set; }
    public DistributionTest DistributionTest { get; set; }
}