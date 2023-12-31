﻿using Application.Common.Interfaces.Persistance;
using Application.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Infrastructure.Persistance.Repositories;

public sealed class AccountRepository : DataContextRepository<Account>, IAccountRepository
{
    public AccountRepository(IDataContext dataContext) : base(dataContext)
    {
    }

    public Account? FindById(int id)
    {
        return Entities
            .Include(x => x.Groups)
            .FirstOrDefault(x => x.Id == id);
    }

    public Account Insert(Account value)
    {
        var entry = Entities.Add(value);
        SaveChanges();
        return entry.Entity;
    }

    public Account? Update(int id, Account value)
    {
        var entity = FindById(id);

        if (entity is null)
            return null;

        entity.Name = value.Name;
        entity.Login = value.Login;
        entity.Email = value.Email;
        entity.Password = value.Password;

        SaveChanges();
        return entity;
    }

    public bool Delete(Account value)
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

    public Account? FindByEmail(string email)
    {
        return Entities
            .Include(x => x.Groups)
            .FirstOrDefault(x => x.Email == email);
    }

    public Account? FindByLogin(string login)
    {
        return Entities
            .Include(x => x.Groups)
            .FirstOrDefault(x => x.Login == login);
    }

    public bool IsEmailExist(string email)
    {
        return FindByEmail(email) is not null;
    }

    public bool IsLoginExist(string login)
    {
        return FindByLogin(login) is not null;
    }

    public Account? Find(Func<Account, bool> expression)
    {
        return Entities
            .Include(x => x.Groups)
            .FirstOrDefault(expression);
    }

    public List<Account> FindAll()
    {
        return Entities
            .AsNoTracking()
            .Include(x => x.Groups)
            .ToList();
    }

    public List<Account> FindAll(Func<Account, bool> expression)
    {
        return Entities
            .Include(x => x.Groups)
            .Where(expression)
            .ToList();
    }
}
