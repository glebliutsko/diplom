namespace ExamPapers.ServerAPI.DataAccess;

public interface IDataAccesser<T> where T: class
{
    public Task<IEnumerable<T>> GetAll();
    public Task<T?> GetById(int id);
    
    public Task Create(T item);
    public void Update(T item);
    public void Delete(T item);
    public Task DeleteById(int id);

    public Task Save();
}