using Application.Entities;

namespace Application.Common.Interfaces.Persistance;

public interface IGroupRepository : IRepository<Group>
{
    bool AddAccountToGroup(Account account, int groupId);
    bool AddAccountToGroup(Account account, Group group);
    
    bool RemoveAccountFromGroup(Account account, int groupId);
    bool RemoveAccountFromGroup(Account account, Group group);
}
