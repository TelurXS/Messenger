using Application.Entities;
using Application.Features.Messages;

namespace Application.Common.Interfaces.Mappings;

public interface IMessageMapper
{
    Message FromRequest(CreateMessageAtGroupFromAccount.Request request);
    
    Message FromRequest(UpdateMessage.Request request);
}