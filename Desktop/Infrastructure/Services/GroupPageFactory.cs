using Application.Entities;
using Desktop.Interfaces;
using Desktop.Pages;
using MediatR;

namespace Desktop.Infrastructure.Services;

public class GroupPageFactory : IGroupPageFactory
{
    public GroupPageFactory(
        IMediator mediator, 
        ICurrentGroupService currentGroupService, 
        ICurrentUserService currentUserService)
    {
        Mediator = mediator;
        CurrentGroupService = currentGroupService;
        CurrentUserService = currentUserService;
    }
    
    private IMediator Mediator { get; }
    private ICurrentGroupService CurrentGroupService { get; }
    private ICurrentUserService CurrentUserService { get; }
    
    public GroupPage Create(Group group)
    {
        return new GroupPage(group, Mediator, CurrentGroupService, CurrentUserService);
    }
}