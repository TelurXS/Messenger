using Application.Common.Interfaces.Persistance;
using Application.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Infrastructure.Persistance;

public sealed class DataContext : DbContext, IDataContext
{
    public DataContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Account> Accounts { get; set; }
    public DbSet<Message> Messages { get; set; }
    public DbSet<Group> Groups { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(IAssemblyMark).Assembly);

        base.OnModelCreating(modelBuilder);
    }
}
