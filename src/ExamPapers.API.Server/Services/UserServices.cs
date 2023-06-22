using ExamPapers.API.Server.DataAccess;
using ExamPapers.API.Server.Mapper;
using ExamPapers.API.Server.Utils.PasswordHash;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ExamPapers.API.Server.Services;

public class UserServices
{
    private readonly UserDataAccesser _userDataAccesser;
    private readonly ExamPapersDbContext _db;
    private readonly IPasswordHasher _hasher;

    public UserServices(UserDataAccesser userDataAccesser, ExamPapersDbContext db)
    {
        _userDataAccesser = userDataAccesser;
        _db = db;
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

        if (string.IsNullOrEmpty(newUser.Login) && string.IsNullOrEmpty(newUser.FullName))
            return false;

        ORMModels.Group? group = null;
        if (newUser.GroupName != null)
        {
            group = await _db.Groups.FirstOrDefaultAsync(x => x.Name == newUser.GroupName);

            if (group == null)
            {
                group = new ORMModels.Group { Name = newUser.GroupName };
            }
        }
        else if (newUser.GroupId != null)
        {
            group = await _db.Groups.FirstOrDefaultAsync(x => x.Id == newUser.GroupId);
        }

        var dbUser = new ORMModels.User
        {
            FullName = newUser.FullName,
            Login = newUser.Login,
            Role = role,
            Group = group
        };

        if (!string.IsNullOrEmpty(newUser.Password))
            dbUser.PasswordHash = _hasher.GenerateHashPassword(newUser.Password).Hex;

        await _userDataAccesser.Create(dbUser);
        await _userDataAccesser.Save();

        return true;
    }

    public async Task<bool> EditUser(int id, NewUserRequest editedUser)
    {
        var user = await _userDataAccesser.GetById(id);
        if (user == null)
            return false;

        ORMModels.Role? role = null;
        if (Enum.TryParse(editedUser.Role, out ORMModels.Role roleT))
            role = roleT;
            

        if (!string.IsNullOrEmpty(editedUser.FullName))
            user.FullName = editedUser.FullName;
        
        if (!string.IsNullOrEmpty(editedUser.Login))
            user.Login = editedUser.Login;
        
        if (role != null)
            user.Role = (ORMModels.Role)role;
        
        if (!string.IsNullOrEmpty(editedUser.Password))
            user.PasswordHash = _hasher.GenerateHashPassword(editedUser.Password).Hex;

        if (editedUser.GroupId != null)
            user.Group = await _db.Groups.FirstOrDefaultAsync(x => x.Id == editedUser.GroupId) ?? new ORMModels.Group { Name = editedUser.GroupName };

        await _userDataAccesser.Save();
        return true;
    }

    public async Task<bool> DeleteUser(int id)
    {
        await _userDataAccesser.DeleteById(id);
        await _userDataAccesser.Save();

        return true;
    }
}