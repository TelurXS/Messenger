using Application.Common.Interfaces;
using Application.Common.Interfaces.Services;
using Application.Common.Models.Results;
using Application.Common.Models.Results.Unions;
using Application.Entities;
using MediatR;

namespace Application.Features.Messages;

public static class GetLastMessagesFromGroup
{
    public class Request : IRequest<GetAllResult<Message>>
    {
        public int GroupId { get; set; }
        
        public int Count { get; set; } = 100;
    }
    
    public class Handler : ISyncRequestHandler<Request, GetAllResult<Message>>
    {
        public Handler(IGroupService groupService, IMessageService messageService)
        {
            GroupService = groupService;
            MessageService = messageService;
        }
        
        private IGroupService GroupService { get; }
        private IMessageService MessageService { get; }
        
        public Task<GetAllResult<Message>> Handle(Request request, CancellationToken cancellationToken)
        {
            return Task.FromResult(Handle(request));
        }

        public GetAllResult<Message> Handle(Request request)
        {
            var group = GroupService.FindById(request.GroupId);

            if (group.NotFound)
                return new NotFound();

            var result = MessageService.FindLastFromGroup(request.GroupId, request.Count);

            return result;
        }
    }
}