using Application.Common.Models.Results.Unions;
using Application.Entities;

namespace Application.Common.Interfaces.Services;

public interface IMessageService : IService<Message>
{
    GetAllResult<Message> FindLastFromGroup(int groupId, int count = 100);
}
