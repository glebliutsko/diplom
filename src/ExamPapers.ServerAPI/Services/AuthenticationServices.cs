using ExamPapers.ServerAPI.DataAccess;
using ExamPapers.ServerAPI.Utils;
using ExamPapers.ServerAPI.Utils.PasswordHash;

namespace ExamPapers.ServerAPI.Services;

public class AuthenticationServices : IAuthenticationServices
{
    private const int LENGTH_TOKEN = 70;
    private static readonly TimeSpan PERIOD_TOKEN = new TimeSpan(30, 0, 0, 0);

    private readonly IPasswordHasher _hasher;
    private readonly RandomString _tokenGenerator;
    
    private readonly UserDataAccesser _userDataAccesser;
    private readonly TokenDataAccesser _tokenDataAccesser;

    public AuthenticationServices(UserDataAccesser userDataAccesser, TokenDataAccesser tokenDataAccesser, RandomString tokenGenerator, IPasswordHasher hasher)
    {
        _userDataAccesser = userDataAccesser;
        _tokenDataAccesser = tokenDataAccesser;
        _tokenGenerator = tokenGenerator;
        _hasher = hasher;
    }

    public AuthenticationServices(UserDataAccesser userDataAccesser, TokenDataAccesser tokenDataAccesser) :
        this(userDataAccesser, tokenDataAccesser, new RandomString(), new PasswordHasher())
    {
    }

    public async Task<ORMModels.User?> CheckCredentials(string login, string password)
    {
        var user = await _userDataAccesser.GetByLogin(login);
        if (user == null)
            return null;

        if (_hasher.ValidatePassword(password, user.PasswordHash))
            return user;

        return null;
    }

    public async Task<Token> IssueToken(int forUserId)
    {
        var forUser = await _userDataAccesser.GetById(forUserId);
        if (forUser == null)
            throw new Exception("User not found"); // TODO: Кидать нормальное исключение.
        
        var accessToken = _tokenGenerator.Generate(70);
        var expire = DateTime.UtcNow + PERIOD_TOKEN; // TODO: Удалять милисекунды из даты. Они там не нужны. 

        var ormToken = new ORMModels.Token
        {
            User = forUser,
            Value = accessToken,
            Expire = expire,
        };
        await _tokenDataAccesser.Create(ormToken);
        await _tokenDataAccesser.Save();
        
        return new Token
        {
            AccessToken = ormToken.Value,
            Expire = ormToken.Expire,
            User = new User
            {
                Id = forUser.Id,
                Login = forUser.Login
            }
        };
    }
}