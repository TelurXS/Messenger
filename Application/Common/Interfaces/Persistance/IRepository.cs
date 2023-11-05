namespace Application.Common.Interfaces.Persistance;

public interface IRepository<T>
{
    public T? FindById(int id);

    public T? Find(Func<T, bool> expression);

    public List<T> FindAll(Func<T, bool> expression);

    public T Insert(T value);

    public T? Update(int id, T value);

    public bool Delete(T value);

    public bool Delete(int id);
}
