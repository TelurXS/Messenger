using Application.Common.Interfaces.Persistance;
using Application.Common.Interfaces.Services;
using Application.Common.Models.Results.Unions;
using Application.Entities;
using Application.Common.Models.Results;

namespace Application.Infrastructure.Services;

public class GroupService : RepositoryService<Group, IGroupRepository>, IGroupService
{
    public GroupService(IGroupRepository repository) 
        : base(repository)
    {
    }

    public GetResult<Group> FindById(int id)
    {
        var group = Repository.FindById(id);

        if (group is null)
            return new NotFound();

        return group;
    }

    public CreateResult<Group> Create(Group value)
    {
        var entity = Repository.Insert(value);

        if (entity is null)
            return new Failed();

        return entity;
    }

    public UpdateResult<Group> Update(int id, Group value)
    {
        var entity = Repository.FindById(id);

        if (entity is null)
            return new NotFound();

        var result = Repository.Update(id, value);

        if (result is null)
            return new Failed();

        return entity;
    }

    public DeleteResult Delete(Group value)
    {
        var result = Repository.Delete(value);

        if (result is false)
            return new Failed();

        return new Success();
    }

    public DeleteResult Delete(int id)
    {
        var entity = Repository.FindById(id);

        if (entity is null)
            return new NotFound();

        var result = Repository.Delete(entity);

        if (result is false)
            return new Failed();

        return new Success();
    }

    public GetResult<Group> Find(Func<Group, bool> expression)
    {
        var result = Repository.Find(expression);

        if (result is null)
            return new NotFound();

        return result;
    }

    public GetAllResult<Group> FindAll()
    {
        var result = Repository.FindAll();
        
        return result;
    }

    public GetAllResult<Group> FindAll(Func<Group, bool> expression)
    {
        var result = Repository.FindAll(expression);
        
        return result;
    }

    public UpdateResult<Group> AddAccountToGroup(Account account, Group group)
    {
        var result = Repository.AddAccountToGroup(account, group);

        if (result is false)
            return new Failed();

        return group;
    }

    public UpdateResult<Group> RemoveAccountFromGroup(Account account, Group group)
    {
        var result = Repository.RemoveAccountFromGroup(account, group);

        if (result is false)
            return new Failed();

        return group;
    }
}
