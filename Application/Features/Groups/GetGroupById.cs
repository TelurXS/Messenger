using Application.Common.Interfaces;
using Application.Common.Interfaces.Services;
using Application.Common.Models.Results.Unions;
using Application.Entities;
using MediatR;

namespace Application.Features.Groups;

public class GetGroupById
{
    public class Request : IRequest<GetResult<Group>>
    {
        public int Id { get; set; }
    }
    
    public class Handler : ISyncRequestHandler<Request, GetResult<Group>>
    {
        public Handler(IGroupService groupService)
        {
            GroupService = groupService;
        }

        private IGroupService GroupService { get; }
        
        public Task<GetResult<Group>> Handle(Request request, CancellationToken cancellationToken)
        {
            return Task.FromResult(Handle(request));
        }

        public GetResult<Group> Handle(Request request)
        {
            var result = GroupService.FindById(request.Id);

            return result;
        }
    }
}