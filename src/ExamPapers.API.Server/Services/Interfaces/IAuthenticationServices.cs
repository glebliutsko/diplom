namespace ExamPapers.API.Server.Services;

public interface IAuthenticationServices
{
    public Task<UserResponse?> CheckCredentials(string login, string password);
    public Task<TokenResponse> IssueToken(int forUserId);
    public Task<UserResponse?> ValidateToken(string token);
    public Task CancellationToken(string token);
}