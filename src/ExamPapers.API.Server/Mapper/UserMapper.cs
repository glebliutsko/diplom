namespace ExamPapers.API.Server.Mapper;

public static class UserMapper
{
    public static UserResponse OrmModel2ApiEntity(ORMModels.User user)
    {
        return new UserResponse
        {
            Id = user.Id,
            Login = user.Login,
            FullName = user.FullName
        };
    }
}