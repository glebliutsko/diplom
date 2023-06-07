using ExamPapers.API.Server.Services;
using ExamPapers.API.Server.Utils.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExamPapers.API.Server.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthenticationController : ControllerBase
{
    private readonly IAuthenticationServices _authenticationServices;

    public AuthenticationController(IAuthenticationServices authenticationServices)
    {
        _authenticationServices = authenticationServices;
    }
    
    /// <summary>
    /// Получение токена.
    /// </summary>
    /// <param name="credential">Учетные данные пользователя</param>
    /// <returns></returns>
    /// <response code="200">Успешная авторизация</response>
    /// <response code="401">Неверный логин или пароль</response>
    [HttpGet]
    [Route("[action]")]
    [ProducesResponseType(typeof(Token), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorsList), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Token([FromQuery] Credential credential)
    {
        if (User.Identity?.IsAuthenticated == true)
        {
            return StatusCode(StatusCodes.Status403Forbidden, new ErrorsList
            {
                StatusCode = StatusCodes.Status403Forbidden,
                ErrorCode = "AlreadyAuthorized",
                Errors = new List<Error> { new() { Detail = "User already authorized" }}
            });
        }
        
        var user = await _authenticationServices.CheckCredentials(credential.Login, credential.Password);
        if (user == null)
        {
            return Unauthorized(new ErrorsList
            {
                StatusCode = 401,
                ErrorCode = "InvalidCredential",
                Errors = new List<Error>
                { new() { Detail = "Invalid login or password." } }
            });
        }
        
        var token = await _authenticationServices.IssueToken(user.Id);
        return Ok(token);
    }

    /// <summary>
    /// Выход. Деактивация токена.
    /// </summary>
    /// <returns></returns>
    /// <response code="201">Токен деактивирован</response>
    /// <response code="401">Ошибка авторизации</response>
    [HttpGet]
    [Route("[action]")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Logout()
    {
        var token = User.GetToken()!;
        await _authenticationServices.CancellationToken(token);
        
        return NoContent();
    }
}