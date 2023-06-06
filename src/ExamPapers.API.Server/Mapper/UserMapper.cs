namespace ExamPapers.API.Server.Mapper;

public static class UserMapper
{
    public static User OrmModel2ApiEntity(ORMModels.User user)
    {
        return new User
        {
            Id = user.Id,
            Login = user.Login
        };
    }
}