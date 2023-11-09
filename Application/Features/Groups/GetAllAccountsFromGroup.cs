using Application.Common.Interfaces;
using Application.Common.Interfaces.Services;
using Application.Common.Models.Results;
using Application.Common.Models.Results.Unions;
using Application.Entities;
using MediatR;

namespace Application.Features.Groups;

public static class GetAllAccountsFromGroup
{
    public class Request : IRequest<GetAllResult<Account>>
    {
        public int GroupId { get; set; }
    }
    
    public class Handler : ISyncRequestHandler<Request, GetAllResult<Account>>
    {
        public Handler(IGroupService groupService)
        {
            GroupService = groupService;
        }
        
        private IGroupService GroupService { get; }
        
        public Task<GetAllResult<Account>> Handle(Request request, CancellationToken cancellationToken)
        {
            return Task.FromResult(Handle(request));
        }

        public GetAllResult<Account> Handle(Request request)
        {
            var foundResult = GroupService.FindById(request.GroupId);

            if (foundResult.NotFound)
                return new NotFound();

            var group = foundResult.AsFound;
            var accounts = group.Accounts;

            return accounts;
        }
    }
}