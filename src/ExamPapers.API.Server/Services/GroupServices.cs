using ExamPapers.API.Server.Mapper;
using Microsoft.EntityFrameworkCore;

namespace ExamPapers.API.Server.Services;

public class GroupServices
{
    private readonly ExamPapersDbContext _db;

    public GroupServices(ExamPapersDbContext db)
    {
        _db = db;
    }

    public async Task<GroupResponse[]> GetAllGroup()
    {
        var groups = await _db.Groups.ToListAsync();
        return groups.Select(GroupMapper.OrmModel2ApiEntity).ToArray();
    }
}