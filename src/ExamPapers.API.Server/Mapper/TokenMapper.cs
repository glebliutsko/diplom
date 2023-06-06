namespace ExamPapers.API.Server.Mapper;

public static class TokenMapper
{
    public static Token OrmModel2ApiEntity(ORMModels.Token token, User user)
    {
        ArgumentNullException.ThrowIfNull(token);
        ArgumentNullException.ThrowIfNull(user);
        
        return new Token
        {
            AccessToken = token.Value,
            Expire = token.Expire,
            User = user
        };
    }

    public static Token OrmModel2ApiEntity(ORMModels.Token token, ORMModels.User user)
    {
        return OrmModel2ApiEntity(token, UserMapper.OrmModel2ApiEntity(user));
    }
}