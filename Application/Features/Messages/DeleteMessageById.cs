using Application.Common.Interfaces;
using Application.Common.Interfaces.Services;
using Application.Common.Models.Results.Unions;
using Application.Entities;
using MediatR;

namespace Application.Features.Messages;

public static class DeleteMessageById
{
    public class Request : IRequest<DeleteResult>
    {
        public int Id { get; set; }
    }
    
    public class Handler : ISyncRequestHandler<Request, DeleteResult>
    {
        public Handler(IMessageService messageService)
        {
            MessageService = messageService;
        }
        
        private IMessageService MessageService { get; }

        public Task<DeleteResult> Handle(Request request, CancellationToken cancellationToken)
        {
            return Task.FromResult(Handle(request));
        }

        public DeleteResult Handle(Request request)
        {
            var result = MessageService.Delete(request.Id);

            return result;
        }
    }
}