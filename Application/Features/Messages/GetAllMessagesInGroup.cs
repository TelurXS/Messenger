using Application.Common.Interfaces;
using Application.Common.Interfaces.Services;
using Application.Common.Models.Results;
using Application.Common.Models.Results.Unions;
using Application.Entities;
using Application.Infrastructure.Services;
using MediatR;

namespace Application.Features.Messages;

public static class GetAllMessagesInGroup
{
    public class Request : IRequest<GetAllResult<Message>>
    {
        public int GroupId { get; set; }
    }
    
    public class Handler : ISyncRequestHandler<Request, GetAllResult<Message>>
    {
        public Handler(IMessageService messageService, IGroupService groupService)
        {
            MessageService = messageService;
            GroupService = groupService;
        }
        
        private IMessageService MessageService { get; }
        private IGroupService GroupService { get; }

        public Task<GetAllResult<Message>> Handle(Request request, CancellationToken cancellationToken)
        {
            return Task.FromResult(Handle(request));
        }

        public GetAllResult<Message> Handle(Request request)
        {
            var group = GroupService.FindById(request.GroupId).AsFound;

            var messages = group.Messages;

            if (messages.Count == 0)
                return new NotFound();

            return messages;
        }
    }
}