using ExamPapers.API.Server.Mapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ExamPapers.API.Server.Controllers;

[ApiController]
[Route("[controller]")]
public class TestingSessionController : ControllerBase
{
    private readonly ExamPapersDbContext _db;

    public TestingSessionController(ExamPapersDbContext db)
    {
        _db = db;
    }

    [HttpGet]
    [Route("all")]
    [Authorize(Roles = "Teacher")]
    public async Task<IActionResult> GetAllMySession()
    {
        var teacherId = (int)User.GetId()!;

        var sessions = await _db.TestingSessions
            .Include(x => x.Test).ThenInclude(x => x.QuestionsInTests)
            .Include(x => x.DistributionTests).ThenInclude(x => x.Results)
            .Include(x => x.DistributionTests).ThenInclude(x => x.Student)
            .Where(x => x.Test.OwnerId == teacherId)
            .OrderByDescending(x => x.DistributionDate)
            .ToArrayAsync();

        return Ok(sessions.Select(session => new TestSessionResponse
        {
            Id = session.Id,
            DistributionDate = session.DistributionDate,
            Deadline = session.Deadline,
            Test = new TestShortResponse
            {
                Id = session.Test.Id,
                Title = session.Test.Title,
                Description = session.Test.Description,
                CountQuestion = session.Test.QuestionsInTests.Count
            },
            Distributions = session.DistributionTests.Select(distribution => new DistributionResponse
            {
                Id = distribution.Id,
                Student = UserMapper.OrmModel2ApiEntity(distribution.Student),
                Result = distribution.Results.Count == 0
                    ? null
                    : new TestingResultResponse
                    {
                        Score = distribution.Results[0].Score,
                        AllScore = distribution.Results[0].AllScore,
                        PassedTime = distribution.Results[0].PassedTime
                    }
            }).ToList()
        }).ToList());
    }
}