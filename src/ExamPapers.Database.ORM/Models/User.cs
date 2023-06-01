namespace ExamPapers.Database.ORM.Models;

#nullable disable

[Table("users")]
public class User
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(255)]
    public string Login { get; set; }
    
    [Required]
    [StringLength(255)]
    public string PasswordHash { get; set; }
}