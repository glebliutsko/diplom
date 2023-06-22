using Microsoft.EntityFrameworkCore;

namespace ExamPapers.API.Server.DataAccess;

public class UserDataAccesser : IDataAccesser<ORMModels.User>
{
    private readonly ExamPapersDbContext _db;

    public UserDataAccesser(ExamPapersDbContext db)
    {
        _db = db;
    }

    public async Task<ORMModels.User?> GetByLogin(string login)
    {
        return await _db.Users
            .Include(x => x.Group)
            .FirstOrDefaultAsync(x => x.Login == login);
    }

    public async Task<IEnumerable<ORMModels.User>> GetAll()
    {
        return await _db.Users
            .Include(x => x.Group)
            .ToArrayAsync();
    }

    public async Task<ORMModels.User?> GetById(int id)
    {
        return await _db.Users
            .Include(x => x.Group)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task Create(ORMModels.User item)
    {
        await _db.Users.AddAsync(item);
    }

    public void Update(ORMModels.User item)
    {
        _db.Entry(item).State = EntityState.Modified;
    }

    public void Delete(ORMModels.User item)
    {
        _db.Users.Remove(item);
    }

    public async Task DeleteById(int id)
    {
        var item = await _db.Users.FindAsync(id);
        if (item != null)
            _db.Remove(item);
    }

    public async Task Save()
    {
        await _db.SaveChangesAsync();
    }
}