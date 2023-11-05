using Application.Common.Models.Results.Unions;

namespace Application.Common.Interfaces.Services;

public interface IService<T>
{
    public GetResult<T> FindById(int id);

    public GetResult<T> Find(Func<T, bool> expression);

    public GetAllResult<T> FindAll(Func<T, bool> expression);

    public CreateResult<T> Create(T value);

    public UpdateResult<T> Update(int id, T value);

    public DeleteResult Delete(T value);

    public DeleteResult Delete(int id);
}
