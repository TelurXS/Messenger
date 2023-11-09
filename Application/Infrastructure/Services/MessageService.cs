using Application.Common.Interfaces.Persistance;
using Application.Common.Interfaces.Services;
using Application.Common.Models.Results;
using Application.Common.Models.Results.Unions;
using Application.Entities;

namespace Application.Infrastructure.Services;

public class MessageService : RepositoryService<Message, IMessageRepository>, IMessageService
{
    public MessageService(IMessageRepository repository) 
        : base(repository)
    {
    }

    public GetResult<Message> FindById(int id)
    {
        var account = Repository.FindById(id);

        if (account is null)
            return new NotFound();

        return account;
    }

    public CreateResult<Message> Create(Message value)
    {
        var entity = Repository.Insert(value);

        if (entity is null)
            return new Failed();

        return entity;
    }

    public UpdateResult<Message> Update(int id, Message value)
    {
        var entity = Repository.FindById(id);

        if (entity is null)
            return new NotFound();

        var result = Repository.Update(id, value);

        if (result is null)
            return new Failed();

        return entity;
    }

    public DeleteResult Delete(Message value)
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

    public GetResult<Message> Find(Func<Message, bool> expression)
    {
        var result = Repository.Find(expression);

        if (result is null)
            return new NotFound();

        return result;
    }

    public GetAllResult<Message> FindAll()
    {
        var result = Repository.FindAll();
        
        if (result.Count == 0)
            return new NotFound();

        return result;
    }

    public GetAllResult<Message> FindAll(Func<Message, bool> expression)
    {
        var result = Repository.FindAll(expression);

        if (result.Count == 0)
            return new NotFound();

        return result;
    }
}
