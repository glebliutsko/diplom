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
                Errors = new List<ErrorResponse> { new() { Detail = $"User Id {id} not found" } }
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

    [HttpGet]
    [Authorize(Roles = "Admin, Teacher")]
    [Route("all")]
    public async Task<IActionResult> GetAllUsers()
    {
        return Ok(await _userServices.GetAllUser());
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    [Route("")]
    public async Task<IActionResult> CreateUser(NewUserRequest newUser)
    {
        var result = await _userServices.CreateNewUser(newUser);
        if (result)
            return Ok(new SuccessResponse());

        return BadRequest(new ErrorsListResponse
        {
            StatusCode = StatusCodes.Status400BadRequest,
            ErrorCode = "CreateUserFailed",
            Errors = new List<ErrorResponse> { new() { Detail = $"Error on create user" } }
        });
    }

    [HttpPut]
    [Authorize(Roles = "Admin")]
    [Route("{id:int}")]
    public async Task<IActionResult> EditUser(int id, NewUserRequest editedUser)
    {
        var result = await _userServices.EditUser(id, editedUser);
        if (result)
            return Ok(new SuccessResponse());

        return BadRequest(new ErrorsListResponse
        {
            StatusCode = StatusCodes.Status400BadRequest,
            ErrorCode = "EditUserFailed",
            Errors = new List<ErrorResponse> { new() { Detail = $"Error on edit user" } }
        });
    }

    [HttpDelete]
    [Authorize(Roles = "Admin")]
    [Route("{id:int}")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        await _userServices.DeleteUser(id);
        return Ok(new SuccessResponse());
    }
}
    