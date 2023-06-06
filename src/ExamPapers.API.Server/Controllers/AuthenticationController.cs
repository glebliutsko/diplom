using ExamPapers.API.Server.Services;
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
    /// Аутентификация и получение токена. 
    /// </summary>
    /// <param name="credential">Учетные данные пользователя</param>
    /// <returns></returns>
    /// <response code="200">Успешная авторизация</response>
    /// <response code="401">Неверный логин или пароль</response>
    [HttpPost]
    [Route("[action]")]
    [ProducesResponseType(typeof(Token), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Login(Credential credential)
    {
        var user = await _authenticationServices.CheckCredentials(credential.Login, credential.Password);
        if (user == null)
        {
            return Unauthorized(new Error
            {
                StatusCode = 401,
                ErrorCode = "InvalidCredential",
                Detail = "Invalid login or password."
            });
        }
        
        var token = await _authenticationServices.IssueToken(user.Id);
        return Ok(token);
    }

    [HttpGet]
    [Route("[action]")]
    [Authorize]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    public async Task<IActionResult> Logout()
    {
        
        return Ok(User.Identity?.AuthenticationType);
    }
}