using Application.Entities;

namespace Desktop.Interfaces;

public interface ICurrentGroupService
{
    bool IsSet { get; }
    
    Group? Group { get; }

    public void Set(Group group);
}