using ExamPapers.ServerAPI.DataAccess;
using ExamPapers.ServerAPI.Utils.PasswordHash;

namespace ExamPapers.ServerAPI.Services;

public class AuthenticationServices : IAuthenticationServices
{
    private readonly UserDataAccesser _userDataAccesser;
    private readonly IPasswordHasher _hasher;

    public AuthenticationServices(UserDataAccesser userDataAccesser, IPasswordHasher hasher)
    {
        _userDataAccesser = userDataAccesser;
        _hasher = hasher;
    }

    public AuthenticationServices(UserDataAccesser userDataAccesser) : this(userDataAccesser, new PasswordHasher())
    { }

    public async Task<ORMModels.User?> CheckCredentials(string login, string password)
    {
        var user = await _userDataAccesser.GetByLogin(login);
        if (user == null)
            return null;

        if (_hasher.ValidatePassword(password, user.PasswordHash))
            return user;
        
        return null;
    }
}