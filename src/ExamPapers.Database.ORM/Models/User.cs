namespace ExamPapers.Database.ORM.Models;

#nullable disable

[Table("users")]
public class User
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(255)]
    public string FullName { get; set; }
    
    [Required]
    [StringLength(255)]
    public string Login { get; set; }

    [StringLength(144)]
    public string PasswordHash { get; set; }

    public int? GroupId { get; set; }
    public Group Group { get; set; }
    
    [Required]
    public Role Role { get; set; }

    public ICollection<Token> Tokens { get; set; }
}