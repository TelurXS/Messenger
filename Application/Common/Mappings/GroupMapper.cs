using Application.Common.Interfaces.Mappings;
using Application.Entities;
using Application.Features.Groups;
using Riok.Mapperly.Abstractions;

namespace Application.Common.Mappings;

[Mapper]
public partial class GroupMapper : IGroupMapper
{
    public partial Group FromRequest(CreateGroup.Request request);
    
    public partial Group FromRequest(UpdateGroup.Request request);
}