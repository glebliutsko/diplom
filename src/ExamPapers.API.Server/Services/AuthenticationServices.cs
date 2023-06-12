using ExamPapers.API.Server.DataAccess;
using ExamPapers.API.Server.Mapper;
using ExamPapers.API.Server.Utils;
using ExamPapers.API.Server.Utils.PasswordHash;

namespace ExamPapers.API.Server.Services;

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

    public async Task<UserResponse?> CheckCredentials(string login, string password)
    {
        var user = await _userDataAccesser.GetByLogin(login);
        if (user == null)
            return null;

        if (_hasher.ValidatePassword(password, user.PasswordHash))
            return UserMapper.OrmModel2ApiEntity(user);

        return null;
    }

    public async Task<TokenResponse> IssueToken(int forUserId)
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

        return TokenMapper.OrmModel2ApiEntity(ormToken, forUser);
    }

    public async Task<UserResponse?> ValidateToken(string tokenValue)
    {
        var token = await _tokenDataAccesser.GetByValue(tokenValue);

        if (token == null)
            return null;
        
        if (token.Expire < DateTime.UtcNow)
            return null; // TODO: Кидание исключения, а не null. 

        var user = (await _userDataAccesser.GetById(token.UserId))!;
        return UserMapper.OrmModel2ApiEntity(user);
    }

    public async Task CancellationToken(string token)
    {
        await _tokenDataAccesser.DeleteByValue(token);
        await _tokenDataAccesser.Save();
    }
}