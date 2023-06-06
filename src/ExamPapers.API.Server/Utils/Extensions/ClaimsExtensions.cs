using System.Security.Claims;

namespace ExamPapers.API.Server.Utils.Extensions;

public static class ClaimsExtensions
{
    public static string? GetToken(this ClaimsPrincipal claims)
    {
        return claims.Claims
            .FirstOrDefault(x => x.Type == "Token")?
            .Value;
    }
}