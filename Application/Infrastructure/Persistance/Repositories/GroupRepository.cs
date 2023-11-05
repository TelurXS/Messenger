using Application.Common.Interfaces.Persistance;
using Application.Entities;
using System.Linq;

namespace Application.Infrastructure.Persistance.Repositories;

public sealed class GroupRepository : DataContextRepository<Group>, IGroupRepository
{
    public GroupRepository(IDataContext dataContext) : base(dataContext)
    {
    }

    public Group? FindById(int id)
    {
        return Entities.FirstOrDefault(x => x.Id == id);
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
        return Entities.FirstOrDefault(expression);
    }

    public List<Group> FindAll(Func<Group, bool> expression)
    {
        return Entities.Where(expression).ToList();
    }
}
