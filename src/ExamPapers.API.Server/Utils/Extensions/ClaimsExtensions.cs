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
    
    public static string? GetLogin(this ClaimsPrincipal claims)
    {
        return claims.Claims
            .FirstOrDefault(x => x.Type == ClaimTypes.Name)?
            .Value;
    }
    
    public static int? GetId(this ClaimsPrincipal claims)
    {
        var id = claims.Claims
            .FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?
            .Value;

        if (int.TryParse(id, out int intId))
            return intId;
        return null;
    }
}