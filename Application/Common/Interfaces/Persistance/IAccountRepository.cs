using Application.Entities;

namespace Application.Common.Interfaces.Persistance;

public interface IAccountRepository : IRepository<Account>
{
    Account? FindByEmail(string email);
    Account? FindByLogin(string login);

    bool IsEmailExist(string email);
    bool IsLoginExist(string login);
}
