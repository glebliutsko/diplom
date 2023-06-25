using ExamPapers.API.Server.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExamPapers.API.Server.Controllers;

[ApiController]
[Route("[controller]")]
public class QuestionController : ControllerBase
{
    private readonly QuestionService _questionService;

    public QuestionController(QuestionService questionService)
    {
        _questionService = questionService;
    }

    [HttpGet]
    [Route("all")]
    [Authorize(Roles = "Teacher")]
    public async Task<IActionResult> GetAll()
    {
        var userId = User.GetId();
        var question = await _questionService.GetAllUsersQuestions((int)userId);
        return Ok(question);
    }
}