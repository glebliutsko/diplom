namespace ExamPapers.Database.ORM.Models;

#nullable disable
[Table("distributionTests")]
public class DistributionTest
{
    [Key]
    public int Id { get; set; }

    [ForeignKey(nameof(Student))]
    public int StudentId { get; set; }
    [Required]
    public User Student { get; set; }

    [ForeignKey(nameof(Session))]
    public int SessionId { get; set; }
    [Required]
    public TestingSession Session { get; set; }
}