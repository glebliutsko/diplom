namespace ExamPapers.Database.ORM.Models;

#nullable disable

[Table("token")]
public class Token
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(255)]
    public string Value { get; set; }
    
    [Required]
    public DateTime Expire { get; set; }

    public int UserId { get; set; }
    [Required]
    public User User { get; set; }
}