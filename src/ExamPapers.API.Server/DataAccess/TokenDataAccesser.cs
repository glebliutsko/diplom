using Microsoft.EntityFrameworkCore;

namespace ExamPapers.API.Server.DataAccess;

public class TokenDataAccesser : IDataAccesser<ORMModels.Token>
{
    private readonly ExamPapersDbContext _db;

    public TokenDataAccesser(ExamPapersDbContext db)
    {
        _db = db;
    }

    public async Task<ORMModels.Token?> GetByValue(string value)
    {
        return await _db.Tokens.FirstOrDefaultAsync(x => x.Value == value);
    }

    public async Task<IEnumerable<ORMModels.Token>> GetAll()
    {
        return await _db.Tokens.ToArrayAsync();
    }

    public async Task<ORMModels.Token?> GetById(int id)
    {
        return await _db.Tokens.FindAsync(id);
    }

    public async Task Create(ORMModels.Token item)
    {
        await _db.Tokens.AddAsync(item);
    }

    public void Update(ORMModels.Token item)
    {
        _db.Entry(item).State = EntityState.Modified;
    }

    public void Delete(ORMModels.Token item)
    {
        _db.Tokens.Remove(item);
    }

    public async Task DeleteById(int id)
    {
        var item = await _db.Tokens.FindAsync(id);
        if (item != null)
            _db.Remove(item);
    }

    public async Task Save()
    {
        await _db.SaveChangesAsync();
    }
}