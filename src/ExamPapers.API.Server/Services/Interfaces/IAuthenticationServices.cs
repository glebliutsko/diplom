namespace ExamPapers.API.Server.Services;

public interface IAuthenticationServices
{
    public Task<ORMModels.User?> CheckCredentials(string login, string password);
    public Task<Token> IssueToken(int forUserId);
}