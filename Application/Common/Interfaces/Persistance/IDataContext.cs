using Application.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Interfaces.Persistance;

public interface IDataContext
{
    DbSet<Account> Accounts { get; }
    DbSet<Group> Groups { get; }
    DbSet<Message> Messages { get; }

    int SaveChanges();

    DbSet<T> Set<T>() where T : class;
}
