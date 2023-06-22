using ExamPapers.API.Server.DataAccess;
using ExamPapers.API.Server.Mapper;

namespace ExamPapers.API.Server.Services;

public class UserServices
{
    private readonly UserDataAccesser _userDataAccesser;

    public UserServices(UserDataAccesser userDataAccesser)
    {
        _userDataAccesser = userDataAccesser;
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
}