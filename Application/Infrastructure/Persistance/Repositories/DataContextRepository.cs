using Application.Common.Interfaces.Persistance;
using Microsoft.EntityFrameworkCore;

namespace Application.Infrastructure.Persistance.Repositories;

public abstract class DataContextRepository<T> where T: class
{
    public DataContextRepository(IDataContext dataContext)
    {
        DataContext = dataContext;
        Entities = DataContext.Set<T>();
    }

    private IDataContext DataContext { get; }

    protected DbSet<T> Entities { get; }

    protected int SaveChanges() => DataContext.SaveChanges();
}
