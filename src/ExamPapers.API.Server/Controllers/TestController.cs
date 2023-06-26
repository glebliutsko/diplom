using ExamPapers.API.Server.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ExamPapers.API.Server.Controllers;

[ApiController]
[Route("[controller]")]
public class TestController : ControllerBase
{
    private readonly TestServices _testServices;
    private readonly ExamPapersDbContext _db;

    public TestController(TestServices testServices, ExamPapersDbContext db)
    {
        _testServices = testServices;
        _db = db;
    }

    [HttpGet]
    [Route("all")]
    [Authorize(Roles = "Teacher")]
    public async Task<IActionResult> GetAll()
    {
        var tests = await _testServices.GetAllTest((int)User.GetId());
        return Ok(tests);
    }

    [HttpGet]
    [Route("{id:int}")]
    [Authorize]
    public async Task<IActionResult> GetTest(int id)
    {
        var test = await _testServices.GetTest(id);
        return Ok(test);
    }
    
    [HttpGet]
    [Route("{id:int}/full")]
    [Authorize]
    public async Task<IActionResult> GetFullTest(int id)
    {
        var test = await _db.Tests
            .Include(x => x.QuestionsInTests)
            .ThenInclude(x => x.Question)
            .ThenInclude(x => x.Answers)
            .FirstOrDefaultAsync(x => x.Id == id);

        if (test == null)
            return NotFound(new ErrorsListResponse
            {
                StatusCode = StatusCodes.Status400BadRequest,
                ErrorCode = "NotFountTest",
                Errors = new List<ErrorResponse> { new() { Detail = $"Test with id={id} not found" } }
            });
        
        return Ok(new TestFullResponse
        {
            Id = test.Id,
            Description = test.Description,
            Title = test.Title,
            Questions = test.QuestionsInTests.Select(question => new QuestionResponse
            {
                Id = question.Question.Id,
                Text = question.Question.Text,
                Type = Enum.Parse<QuestionTypeResponse>(question.Question.Type.ToString()),
                Score = question.Scored,
                Answers = question.Question.Answers.Select(answer => new AnswerResponse
                {
                    Text = answer.Text,
                    IsCorrect = answer.IsCorrect
                }).ToList()
            }).ToList()
        });
    }

    [HttpPost]
    [Route("")]
    [Authorize(Roles = "Teacher")]
    public async Task<IActionResult> CreateTest(TestRequest newTest)
    {
        var result = await _testServices.CreateTest(newTest, (int)User.GetId());
        if (result)
            return Ok(new SuccessResponse());
        
        return BadRequest(new ErrorsListResponse
        {
            StatusCode = StatusCodes.Status400BadRequest,
            ErrorCode = "CreateTestFailed",
            Errors = new List<ErrorResponse> { new() { Detail = $"Error on create test" } }
        });
    }

    [HttpPost]
    [Route("{id:int}/distribution")]
    [Authorize(Roles = "Teacher")]
    public async Task<IActionResult> DistributionTest(
        TestSessionRequest newSession,
        int id,
        [FromQuery] int? groupId = null,
        [FromQuery] int? studentId = null)
    {
        bool result = false;
        if (groupId != null)
        {
            result = await _testServices.DistributionTestForGroup(id, (int)groupId, newSession);
        }
        else if (studentId != null)
        {
            result = await _testServices.DistributionTestForStudent(id, (int)studentId, newSession);
        }

        if (result)
            return Ok(new SuccessResponse());
        
        return BadRequest(new ErrorsListResponse
        {
            StatusCode = StatusCodes.Status400BadRequest,
            ErrorCode = "CreateTestingSessionFailed",
            Errors = new List<ErrorResponse> { new() { Detail = $"Error on create testing session" } }
        });
    }
}