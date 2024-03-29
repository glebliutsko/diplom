namespace ExamPapers.API.Server.Utils.PasswordHash;

public interface IPasswordHasher
{
    public HashedPassword GenerateHashPassword(string password);
    public bool ValidatePassword(string password, string hashPassword);
}