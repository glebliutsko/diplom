using ExamPapers.API.Server.Mapper;
using Microsoft.EntityFrameworkCore;

namespace ExamPapers.API.Server.Services;

public class QuestionService
{
    private readonly ExamPapersDbContext _db;

    public QuestionService(ExamPapersDbContext db)
    {
        _db = db;
    }

    public async Task<QuestionResponse[]> GetAllUsersQuestions(int ownerId)
    {
        var question = await _db.Questions
            .Include(x => x.Owner)
            .Include(x => x.Answers)
            .Where(x => x.OwnerId == ownerId)
            .OrderBy(x => x.Id)
            .ToArrayAsync();
        
        return question.Select(QuestionMapper.OrmModel2ApiEntity).ToArray();
    }
}