using ExamPapers.ServerAPI.DataAccess;
using ExamPapers.ServerAPI.Utils.PasswordHash;

namespace ExamPapers.ServerAPI.Services;

public class UserServices : IUserServices
{
    private readonly UserDataAccesser _userDataAccesser;
    private readonly IPasswordHasher _hasher;

    public UserServices(UserDataAccesser userDataAccesser, IPasswordHasher hasher)
    {
        _userDataAccesser = userDataAccesser;
        _hasher = hasher;
    }

    public UserServices(UserDataAccesser userDataAccesser) : this(userDataAccesser, new PasswordHasher())
    { }

    public async Task<ORMModels.User?> CheckCredentials(string login, string password)
    {
        var user = await _userDataAccesser.GetByLogin(login);
        if (user == null)
            return null;

        _hasher.ValidatePassword(password, user.PasswordHash);
        
        return user;
    }
}