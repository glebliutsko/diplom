namespace ExamPapers.Database.ORM.Models;

#nullable disable

[Table("groups")]
public class Group
{
    [Key]
    public int Id { get; set; }

    [Required(AllowEmptyStrings = false)]
    [StringLength(100)]
    public string Name { get; set; }

    public ICollection<User> Users { get; set; }
}