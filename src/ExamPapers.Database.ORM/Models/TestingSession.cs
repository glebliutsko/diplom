namespace ExamPapers.Database.ORM.Models;

[Table("testingSessions")]
public class TestingSession
{
    [Key]
    public int Id { get; set; }

    [Required]
    public DateTime DistributionDate { get; set; }
    public DateTime? Deadline { get; set; }
    
    [ForeignKey(nameof(Test))]
    public int TestId { get; set; }
    [Required]
    public Test Test { get; set; }

    public List<DistributionTest> DistributionTests { get; set; }
}