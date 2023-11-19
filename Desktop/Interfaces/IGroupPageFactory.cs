using Application.Entities;
using Desktop.Pages;

namespace Desktop.Interfaces;

public interface IGroupPageFactory
{
    GroupPage Create(Group group);
}