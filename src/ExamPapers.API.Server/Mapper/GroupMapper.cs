namespace ExamPapers.API.Server.Mapper;

public static class GroupMapper
{
    public static GroupResponse OrmModel2ApiEntity(ORMModels.Group group)
    {
        return new GroupResponse
        {
            Id = group.Id,
            Name = group.Name
        };
    }
}