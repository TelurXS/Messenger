using Application.Common.Interfaces.Mappings;
using Application.Entities;
using Application.Features.Accounts;
using Application.Features.Messages;
using Riok.Mapperly.Abstractions;

namespace Application.Common.Mappings;

[Mapper]
public partial class MessageMapper : IMessageMapper
{
    public partial Message FromRequest(CreateMessageAtGroupFromAccount.Request request);
    
    public partial Message FromRequest(UpdateMessage.Request request);
}