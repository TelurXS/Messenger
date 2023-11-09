using System.Data.Entity;
using Application.Common.Interfaces.Persistance;
using Application.Entities;

namespace Application.Infrastructure.Persistance.Repositories;

public sealed class MessageRepository : DataContextRepository<Message>, IMessageRepository
{
    public MessageRepository(IDataContext dataContext) : base(dataContext)
    {
    }

    public Message? FindById(int id)
    {
        return Entities.FirstOrDefault(x => x.Id == id);
    }

    public Message Insert(Message value)
    {
        var entry = Entities.Add(value);
        SaveChanges();
        return entry.Entity;
    }

    public Message? Update(int id, Message value)
    {
        var entity = FindById(id);

        if (entity is null)
            return null;

        entity.Content = value.Content;
        entity.SentAt = value.SentAt;

        SaveChanges();
        return entity;
    }

    public bool Delete(Message value)
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

    public Message? Find(Func<Message, bool> expression)
    {
        return Entities.FirstOrDefault(expression);
    }

    public List<Message> FindAll()
    {
        return Entities.AsNoTracking().ToList();
    }

    public List<Message> FindAll(Func<Message, bool> expression)
    {
        return Entities.Where(expression).ToList();
    }
}
