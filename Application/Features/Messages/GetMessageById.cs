using Application.Common.Interfaces;
using Application.Common.Interfaces.Services;
using Application.Common.Models.Results.Unions;
using Application.Entities;
using MediatR;

namespace Application.Features.Messages;

public static class GetMessageById
{
    public class Request : IRequest<GetResult<Message>>
    {
        public int Id { get; set; }
    }
    
    public class Handler : ISyncRequestHandler<Request, GetResult<Message>>
    {
        public Handler(IMessageService messageService)
        {
            MessageService = messageService;
        }
        
        private IMessageService MessageService { get; }

        public Task<GetResult<Message>> Handle(Request request, CancellationToken cancellationToken)
        {
            return Task.FromResult(Handle(request));
        }

        public GetResult<Message> Handle(Request request)
        {
            var result = MessageService.FindById(request.Id);

            return result;
        }
    }
}