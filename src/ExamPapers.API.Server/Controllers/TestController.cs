using ExamPapers.API.Server.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExamPapers.API.Server.Controllers;

[ApiController]
[Route("[controller]")]
public class TestController : ControllerBase
{
    private readonly TestServices _testServices;

    public TestController(TestServices testServices)
    {
        _testServices = testServices;
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
    [Authorize(Roles = "Teacher")]
    public async Task<IActionResult> GetTest(int id)
    {
        var test = await _testServices.GetTest(id);
        return Ok(test);
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