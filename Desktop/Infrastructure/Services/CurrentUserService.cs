using Application.Entities;
using Desktop.Interfaces;

namespace Desktop.Infrastructure.Services;

public sealed class CurrentUserService : ICurrentUserService
{
    public Account? Account { get; private set; }

    public bool IsLogined => Account is not null;
    
    public void Login(Account account)
    {
        Account = account;
    }
}