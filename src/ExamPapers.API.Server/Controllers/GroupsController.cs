using ExamPapers.API.Server.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ExamPapers.API.Server.Controllers;

[ApiController]
[Route("[controller]")]
public class GroupsController : ControllerBase
{
    private readonly GroupServices _groupServices;
    private readonly ExamPapersDbContext _db;

    public GroupsController(GroupServices groupServices, ExamPapersDbContext db)
    {
        _groupServices = groupServices;
        _db = db;
    }
    
    [HttpGet]
    [Authorize(Roles = "Admin, Teacher")]
    [Route("all")]
    public async Task<IActionResult> GetAllUsers()
    {
        return Ok(await _groupServices.GetAllGroup());
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    [Route("")]
    public async Task<IActionResult> CreateGroup(GroupRequest group)
    {
        _db.Add(new ORMModels.Group
        {
            Name = group.Name
        });
        await _db.SaveChangesAsync();

        return Ok(new SuccessResponse());
    }

    [HttpDelete]
    [Authorize(Roles = "Admin")]
    [Route("{id:int}")]
    public async Task<IActionResult> DeleteGroup(int id)
    {
        var groups = await _db.Groups
            .Include(x => x.Users)
            .FirstOrDefaultAsync(x => x.Id == id);
        
        if (groups == null)
        {
            return NotFound(new ErrorsListResponse
            {
                StatusCode = StatusCodes.Status404NotFound,
                ErrorCode = "DeleteGroupFailed",
                Errors = new List<ErrorResponse> { new() { Detail = $"Group id={id} not found" } }
            });
        }

        foreach (var user in groups.Users)
            user.Group = null;
        await _db.SaveChangesAsync();

        _db.Groups.Remove(groups);
        await _db.SaveChangesAsync();
        return Ok(new SuccessResponse());
    }

    [HttpPut]
    [Authorize(Roles = "Admin")]
    [Route("{id:int}")]
    public async Task<IActionResult> EditGroup(int id, GroupRequest editedGroup)
    {
        var group = await _db.Groups.FirstOrDefaultAsync(x => x.Id == id);
        if (group == null)
        {
            return NotFound(new ErrorsListResponse
            {
                StatusCode = StatusCodes.Status404NotFound,
                ErrorCode = "DeleteGroupFailed",
                Errors = new List<ErrorResponse> { new() { Detail = $"Group id={id} not found" } }
            });
        }

        group.Name = editedGroup.Name;
        await _db.SaveChangesAsync();

        return Ok(new SuccessResponse());
    }
}