namespace ExamPapers.ServerAPI.Services;

public interface IUserServices
{
    public Task<ORMModels.User?> CheckCredentials(string login, string password);
}