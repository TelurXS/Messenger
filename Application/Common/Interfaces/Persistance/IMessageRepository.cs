using Application.Common.Models.Results.Unions;
using Application.Entities;

namespace Application.Common.Interfaces.Persistance;

public interface IMessageRepository : IRepository<Message>
{
    List<Message> FindLastFromGroup(int groupId, int count = 100);
}
