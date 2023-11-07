using Application.Common.Models.Results.Unions;
using Application.Entities;

namespace Application.Common.Interfaces.Services;

public interface IAccountService : IService<Account>
{
    GetResult<Account> FindByEmail(string email);
    GetResult<Account> FindByLogin(string login);

    bool IsEmailExist(string email);
    bool IsLoginExist(string login);
    bool IsEmailUnique(string email);
    bool IsLoginUnique(string login);
}
