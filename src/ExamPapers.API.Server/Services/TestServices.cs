using Microsoft.EntityFrameworkCore;

namespace ExamPapers.API.Server.Services;

public class TestServices
{
    private readonly ExamPapersDbContext _db;

    public TestServices(ExamPapersDbContext db)
    {
        _db = db;
    }

    public async Task<TestShortResponse[]> GetAllTest(int ownerId)
    {
        var tests = await _db.Tests
            .Where(x => x.OwnerId == ownerId)
            .Select(x => new TestShortResponse
            {
                Id = x.Id,
                Title = x.Title,
                Description = x.Description,
                CountQuestion = x.QuestionsInTests.Count() 
            })
            .ToArrayAsync();
        
        return tests;
    }

    public async Task<TestWithQuestionResponse?> GetTest(int testId)
    {
        var test = await _db.Tests
            .Include(x => x.QuestionsInTests)
            .ThenInclude(x => x.Question)
            .FirstOrDefaultAsync(x => x.Id == testId);
        
        if (test == null)
            return null;

        return new TestWithQuestionResponse
        {
            Id = test.Id,
            Title = test.Title,
            Description = test.Description,
            Questions = test.QuestionsInTests.Select(x => new QuestionInTestResponse
            {
                Question = new QuestionShortResponse
                {
                    Id = x.Question.Id,
                    Text = x.Question.Text
                },
                Score = x.Scored
            }).ToList()
        };
    }

    public async Task<bool> CreateTest(TestRequest newTest, int ownerId)
    {
        var test = new ORMModels.Test
        {
            Title = newTest.Title,
            Description = newTest.Description,
            QuestionsInTests = newTest.Questions.Select(x => new ORMModels.QuestionsInTest
            {
                QuestionId = x.QuestionId,
                Scored = x.Score
            }).ToList(),
            OwnerId = ownerId
        };

        _db.Tests.Add(test);
        await _db.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DistributionTestForGroup(int testId, int groupId, TestSessionRequest newSession)
    {
        var groupForDistribution = await _db.Groups
            .Include(x => x.Users)
            .FirstOrDefaultAsync(x => x.Id == groupId);

        var testForDistribution = await _db.Tests
            .FirstOrDefaultAsync(x => x.Id == testId);

        if (groupForDistribution == null || testForDistribution == null)
            return false;

        var session = new ORMModels.TestingSession
        {
            DistributionDate = DateTime.UtcNow,
            Deadline = newSession.Deadline,
            TestId = testForDistribution.Id,
            DistributionTests = groupForDistribution.Users
                .Where(x => x.Role == ORMModels.Role.Student)
                .Select(x => new ORMModels.DistributionTest { StudentId = x.Id })
                .ToList()
        };
        _db.TestingSessions.Add(session);
        await _db.SaveChangesAsync();

        return true;
    }
    
    public async Task<bool> DistributionTestForStudent(int testId, int studentId, TestSessionRequest newSession)
    {
        var studentForDistribution = await _db.Users
            .FirstOrDefaultAsync(x => x.Id == studentId && x.Role == ORMModels.Role.Student);

        var testForDistribution = await _db.Tests
            .FirstOrDefaultAsync(x => x.Id == testId);

        if (studentForDistribution == null || testForDistribution == null)
            return false;

        var session = new ORMModels.TestingSession
        {
            DistributionDate = DateTime.UtcNow,
            Deadline = newSession.Deadline,
            TestId = testForDistribution.Id,
            DistributionTests = new List<ORMModels.DistributionTest>
            {
                new() { StudentId = studentForDistribution.Id }
            }
        };
        _db.TestingSessions.Add(session);
        await _db.SaveChangesAsync();

        return true;
    }
}