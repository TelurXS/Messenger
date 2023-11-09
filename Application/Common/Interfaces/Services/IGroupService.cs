using Application.Common.Models.Results.Unions;
using Application.Entities;
using Application.Features.Groups;

namespace Application.Common.Interfaces.Services;

public interface IGroupService : IService<Group>
{
    UpdateResult<Group> AddAccountToGroup(Account account, int groupId);
    UpdateResult<Group> AddAccountToGroup(Account account, Group group);
    UpdateResult<Group> RemoveAccountFromGroup(Account account, int groupId);
    UpdateResult<Group> RemoveAccountFromGroup(Account account, Group group);
}
