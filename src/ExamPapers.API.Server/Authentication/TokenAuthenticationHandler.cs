using System.Security.Claims;
using System.Text.Encodings.Web;
using ExamPapers.API.Server.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;

namespace ExamPapers.API.Server.Authentication;

public class TokenAuthenticationHandler : AuthenticationHandler<TokenAuthOptions>
{
    private readonly IAuthenticationServices _authenticationServices;

    public TokenAuthenticationHandler(IOptionsMonitor<TokenAuthOptions> options, ILoggerFactory logger,
        UrlEncoder encoder, ISystemClock clock, IAuthenticationServices authenticationServices)
        : base(options, logger, encoder, clock)
    {
        _authenticationServices = authenticationServices;
    }

    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        if (!Request.Headers.ContainsKey(Options.TokenHeader))
            return AuthenticateResult.Fail($"Missing Header For Token: {Options.TokenHeader}.");
        
        var tokenHeader = Request.Headers[Options.TokenHeader].First()!;
        var tokenHeaderSpited = tokenHeader.Split(" ");
        if (tokenHeaderSpited.Length != 2 || tokenHeaderSpited[0] != Options.SchemeHeader)
            return AuthenticateResult.Fail($"Invalid syntax header {Options.TokenHeader}.");

        var authorizedUser = await _authenticationServices.ValidateToken(tokenHeaderSpited[1]);
        if (authorizedUser == null)
            return AuthenticateResult.Fail("Incorrect token.");

        var claims = new Claim[]
        {
            new(ClaimTypes.NameIdentifier, authorizedUser.Id.ToString()),
            new(ClaimTypes.Name, authorizedUser.Login),
            new("Token", tokenHeaderSpited[1]),
            new(ClaimTypes.Role, authorizedUser.Role)
        };

        var identity = new ClaimsIdentity(claims, Scheme.Name);
        var principal = new ClaimsPrincipal(identity);
        var ticket = new AuthenticationTicket(principal, Scheme.Name);
        return AuthenticateResult.Success(ticket);
    }
}