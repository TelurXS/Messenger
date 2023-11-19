using Application.Entities;
using Desktop.Interfaces;

namespace Desktop.Infrastructure.Services;

public sealed class CurrentGroupService : ICurrentGroupService
{
    public Group? Group { get; private set; }
    
    public bool IsSet => Group is not null;
    
    public void Set(Group group)
    {
        Group = group;
    }
}