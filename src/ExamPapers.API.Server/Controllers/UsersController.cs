using System.Net;
using ExamPapers.API.Server.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExamPapers.API.Server.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private readonly UserServices _userServices;

    public UsersController(UserServices userServices)
    {
        _userServices = userServices;
    }

    private async Task<IActionResult> ReturnUserById(int id)
    {
        var user = await _userServices.GetUserById(id);
        if (user == null)
            return NotFound(new ErrorsListResponse
            {
                StatusCode = StatusCodes.Status404NotFound,
                ErrorCode = "UserNotFound",
                Errors = new List<ErrorResponse> { new() {Detail = $"User Id {id} not found"} }
            });
        
        return Ok(user);
    }
    
    [HttpGet]
    [Authorize]
    [Route("[action]")]
    public async Task<IActionResult> Me()
    {
        var id = User.GetId();
        return await ReturnUserById((int)id!);
    }

    [HttpGet]
    [Authorize]
    [Route("{id:int}")]
    public async Task<IActionResult> GetUser(int id)
    {
        return await ReturnUserById(id);
    }
}