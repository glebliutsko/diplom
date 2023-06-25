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

    public async Task<bool> CreateQuestion(QuestionRequest newQuestion, int ownerId)
    {
        var answers = newQuestion.Answers
            .Select(x => new ORMModels.Answer { Text = x.Text, IsCorrect = x.IsCorrect })
            .ToList();

        var question = new ORMModels.Question
        {
            Text = newQuestion.Text,
            Type = Enum.Parse<ORMModels.QuestionType>(newQuestion.Type.ToString()),
            Answers = answers,
            OwnerId = ownerId
        };

        _db.Questions.Add(question);
        await _db.SaveChangesAsync();

        return true;
    }
}