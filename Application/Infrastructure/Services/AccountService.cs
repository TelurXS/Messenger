using Application.Common.Interfaces.Persistance;
using Application.Common.Interfaces.Services;
using Application.Common.Models.Results;
using Application.Common.Models.Results.Unions;
using Application.Entities;

namespace Application.Infrastructure.Services;

public class AccountService : RepositoryService<Account, IAccountRepository>, IAccountService
{
    public AccountService(IAccountRepository repository) 
        : base(repository)
    {
    }

    public GetResult<Account> FindById(int id)
    {
        var account = Repository.FindById(id);

        if (account is null)
            return new NotFound();

        return account;
    }

    public CreateResult<Account> Create(Account value)
    {
        var entity = Repository.Insert(value);

        if (entity is null)
            return new Failed();

        return entity;
    }

    public UpdateResult<Account> Update(int id, Account value)
    {
        var entity = Repository.FindById(id);

        if (entity is null)
            return new NotFound();

        var result = Repository.Update(id, value);

        if (result is null)
            return new Failed();

        return entity;
    }

    public DeleteResult Delete(Account value)
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

    public GetResult<Account> Find(Func<Account, bool> expression)
    {
        var result = Repository.Find(expression);

        if (result is null)
            return new NotFound();

        return result;
    }

    public GetAllResult<Account> FindAll()
    {
        var result = Repository.FindAll();

        if (result.Count == 0)
            return new NotFound();
            
        return result;
    }

    public GetAllResult<Account> FindAll(Func<Account, bool> expression)
    {
        var result = Repository.FindAll(expression);

        if (result.Count == 0)
            return new NotFound();

        return result;
    }

    public GetResult<Account> FindByEmail(string email)
    {
        var result = Repository.FindByEmail(email);

        if (result is null)
            return new NotFound();

        return result;
    }

    public GetResult<Account> FindByLogin(string login)
    {
        var result = Repository.FindByLogin(login);

        if (result is null)
            return new NotFound();

        return result;
    }

    public bool IsEmailExist(string email)
    {
        return Repository.FindByEmail(email) is not null;
    }

    public bool IsLoginExist(string login)
    {
        return Repository.FindByLogin(login) is not null;
    }

    public bool IsEmailUnique(string email)
    {
        return Repository.FindByEmail(email) is null;
    }

    public bool IsLoginUnique(string login)
    {
        return Repository.FindByLogin(login) is null;
    }

    public bool IsEmailAvailableForId(int id, string email)
    {
        var entity = Repository.FindById(id);

        if (entity is null)
            return false;

        var found = Repository.FindByEmail(email);

        if (found is null)
            return true;

        return found == entity;
    }

    public bool IsLoginAvailableForId(int id, string login)
    {
        var entity = Repository.FindById(id);

        if (entity is null)
            return false;

        var found = Repository.FindByLogin(login);

        if (found is null)
            return true;

        return found == entity;
    }
}