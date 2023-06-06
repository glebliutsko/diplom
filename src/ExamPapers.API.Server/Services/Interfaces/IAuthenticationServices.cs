namespace ExamPapers.API.Server.Services;

public interface IAuthenticationServices
{
    public Task<User?> CheckCredentials(string login, string password);
    public Task<Token> IssueToken(int forUserId);
    public Task<User?> ValidateToken(string token);
}