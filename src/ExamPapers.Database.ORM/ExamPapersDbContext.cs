using ExamPapers.Database.ORM.Models;

namespace ExamPapers.Database.ORM;

#nullable disable
public class ExamPapersDbContext : DbContext
{
    public ExamPapersDbContext(DbContextOptions options) : base(options)
    {  }

    public DbSet<User> Users { get; set; }
    public DbSet<Token> Tokens { get; set; }
    public DbSet<Group> Groups { get; set; }
}