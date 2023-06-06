using Microsoft.AspNetCore.Authentication;

namespace ExamPapers.API.Server.Authentication;

public class TokenAuthOptions : AuthenticationSchemeOptions
{
    // ReSharper disable once InconsistentNaming
    public const string DefaultSchemeName = "TokenAuthenticationScheme";

    public string TokenHeader { get; set; } = "Authorization";
    public string SchemeHeader { get; set; } = "Bearer";
}