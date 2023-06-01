namespace ExamPapers.Database.ORM;

internal class ExamPapersDbContextFactory : IDesignTimeDbContextFactory<ExamPapersDbContext>
{
    public ExamPapersDbContext CreateDbContext(string[] args)
    {
        var builder = new DbContextOptionsBuilder<ExamPapersDbContext>();
        builder.UseNpgsql(args[0]);

        return new ExamPapersDbContext(builder.Options);
    }
}