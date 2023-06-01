using ExamPapers.Database.ORM.Models;

namespace ExamPapers.Database.ORM;

#nullable disable
public class ExamPapersDbContext : DbContext
{
    public ExamPapersDbContext(DbContextOptions options) : base(options)
    {  }

    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}