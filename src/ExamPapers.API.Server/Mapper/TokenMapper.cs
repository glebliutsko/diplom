namespace ExamPapers.API.Server.Mapper;

public static class TokenMapper
{
    public static TokenResponse OrmModel2ApiEntity(ORMModels.Token token, UserResponse userResponse)
    {
        ArgumentNullException.ThrowIfNull(token);
        ArgumentNullException.ThrowIfNull(userResponse);
        
        return new TokenResponse
        {
            AccessToken = token.Value,
            Expire = token.Expire,
            UserResponse = userResponse
        };
    }

    public static TokenResponse OrmModel2ApiEntity(ORMModels.Token token, ORMModels.User user)
    {
        return OrmModel2ApiEntity(token, UserMapper.OrmModel2ApiEntity(user));
    }
}