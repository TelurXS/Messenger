using Application.Common.Interfaces;
using Application.Common.Interfaces.Services;
using Application.Common.Models.Results.Unions;
using Application.Entities;
using MediatR;

namespace Application.Features.Groups;

public static class GetAllGroups
{
    public class Request : IRequest<GetAllResult<Group>>
    {
    }
    
    public class Handler : ISyncRequestHandler<Request, GetAllResult<Group>>
    {
        public Handler(IGroupService groupService)
        {
            GroupService = groupService;
        }
        
        private IGroupService GroupService { get; }

        public Task<GetAllResult<Group>> Handle(Request request, CancellationToken cancellationToken)
        {
            return Task.FromResult(Handle(request));
        }

        public GetAllResult<Group> Handle(Request request)
        {
            var result = GroupService.FindAll();

            return result;
        }
    }
}