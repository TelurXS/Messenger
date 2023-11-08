using Application.Entities;
using Application.Features.Groups;

namespace Application.Common.Interfaces.Mappings;

public interface IGroupMapper
{
    Group FromRequest(CreateGroup.Request request);
    
    Group FromRequest(UpdateGroup.Request request);
}