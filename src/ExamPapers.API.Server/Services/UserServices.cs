using ExamPapers.API.Server.DataAccess;
using ExamPapers.API.Server.Mapper;
using ExamPapers.API.Server.Utils.PasswordHash;

namespace ExamPapers.API.Server.Services;

public class UserServices
{
    private readonly UserDataAccesser _userDataAccesser;
    private readonly IPasswordHasher _hasher;

    public UserServices(UserDataAccesser userDataAccesser)
    {
        _userDataAccesser = userDataAccesser;
        _hasher = new PasswordHasher();
    }

    public async Task<UserResponse?> GetUserById(int id)
    {
        var user = await _userDataAccesser.GetById(id);
        if (user == null)
            return null;

        return UserMapper.OrmModel2ApiEntity(user);
    }

    public async Task<List<UserResponse>> GetAllUser()
    {
        var users = await _userDataAccesser.GetAll();
        return users.Select(UserMapper.OrmModel2ApiEntity).ToList();
    }

    public async Task<bool> CreateNewUser(NewUserRequest newUser)
    {
        if (!Enum.TryParse(newUser.Role, out ORMModels.Role role))
            return false;

        var dbUser = new ORMModels.User
        {
            FullName = newUser.FullName,
            Login = newUser.Login,
            Role = role
        };

        if (!string.IsNullOrEmpty(newUser.Password))
            dbUser.PasswordHash = _hasher.GenerateHashPassword(newUser.Password).Hex;

        await _userDataAccesser.Create(dbUser);
        await _userDataAccesser.Save();

        return true;
    }
}