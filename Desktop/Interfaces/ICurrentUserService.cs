using Application.Entities;
using Desktop.Pages;

namespace Desktop.Interfaces;

public interface ICurrentUserService
{
    bool IsLogined { get; }
    
    Account? Account { get; }

    public void Login(Account account);
}