using Application.Common.Interfaces.Persistance;
using Application.Entities;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Application.Infrastructure.Persistance.Repositories;

public sealed class GroupRepository : DataContextRepository<Group>, IGroupRepository
{
    public GroupRepository(IDataContext dataContext) : base(dataContext)
    {
    }

    public Group? FindById(int id)
    {
        return Entities
            .Include(x => x.Accounts)
            .Include(x => x.Messages)
            .FirstOrDefault(x => x.Id == id);
    }

    public Group Insert(Group value)
    {
        var entry = Entities.Add(value);
        SaveChanges();
        return entry.Entity;
    }

    public Group? Update(int id, Group value)
    {
        var entity = FindById(id);

        if (entity is null)
            return null;

        entity.Name = value.Name;
        entity.Description = value.Description;

        SaveChanges();
        return entity;
    }

    public bool Delete(Group value)
    {
        Entities.Remove(value);
        return SaveChanges() > 0;
    }

    public bool Delete(int id)
    {
        var entity = FindById(id);

        if (entity is null)
            return false;

        return Delete(entity);
    }

    public Group? Find(Func<Group, bool> expression)
    {
        return Entities
            .Include(x => x.Accounts)
            .Include(x => x.Messages)
            .FirstOrDefault(expression);
    }

    public List<Group> FindAll()
    {
        return Entities
            .AsNoTracking()
            .Include(x => x.Accounts)
            .Include(x => x.Messages)
            .ToList();
    }

    public List<Group> FindAll(Func<Group, bool> expression)
    {
        return Entities
            .AsNoTracking()
            .Include(x => x.Accounts)
            .Include(x => x.Messages)
            .Where(expression)
            .ToList();
    }
    
    public bool AddAccountToGroup(Account account, Group group)
    {
        group.Accounts.Add(account);
        account.Groups.Add(group);
        return SaveChanges() > 0;
    }

    public bool RemoveAccountFromGroup(Account account, Group group)
    {
        group.Accounts.Remove(account);
        account.Groups.Remove(group);
        return SaveChanges() > 0;
    }
}
