using ExamPapers.API.Server.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExamPapers.API.Server.Controllers;

[ApiController]
[Route("[controller]")]
public class GroupsController : ControllerBase
{
    private readonly GroupServices _groupServices;

    public GroupsController(GroupServices groupServices)
    {
        _groupServices = groupServices;
    }
    
    [HttpGet]
    [Authorize(Roles = "Admin")]
    [Route("all")]
    public async Task<IActionResult> GetAllUsers()
    {
        return Ok(await _groupServices.GetAllGroup());
    }
}