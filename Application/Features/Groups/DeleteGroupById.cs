using Application.Common.Interfaces;
using Application.Common.Interfaces.Services;
using Application.Common.Models.Results.Unions;
using Application.Infrastructure.Services;
using MediatR;

namespace Application.Features.Groups;

public static class DeleteGroupById
{
    public class Request : IRequest<DeleteResult>
    {
        public int Id { get; set; }
    }
    
    public class Handler : ISyncRequestHandler<Request, DeleteResult>
    {
        public Handler(IGroupService service)
        {
            GroupService = service;
        }
        
        private IGroupService GroupService { get; }
        
        public Task<DeleteResult> Handle(Request request, CancellationToken cancellationToken)
        {
            return Task.FromResult(Handle(request));
        }

        public DeleteResult Handle(Request request)
        {
            var result = GroupService.Delete(request.Id);

            return result;
        }
    }
}