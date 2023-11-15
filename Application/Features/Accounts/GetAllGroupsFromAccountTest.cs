using Application.Common.Interfaces;
using Application.Common.Interfaces.Services;
using Application.Common.Models.Results;
using Application.Common.Models.Results.Unions;
using Application.Entities;
using MediatR;

namespace Application.Features.Accounts;

public class GetAllGroupsFromAccountTest
{
    public class Request : IRequest<GetAllResult<Group>>
    {
        public int Id { get; set; }
    }
    
    public class Handler : ISyncRequestHandler<Request, GetAllResult<Group>>
    {
        public Handler(IAccountService accountService, IGroupService groupService)
        {
            AccountService = accountService;
            GroupService = groupService;
        }
        
        private IAccountService AccountService { get; }
        private IGroupService GroupService { get; }

        public Task<GetAllResult<Group>> Handle(Request request, CancellationToken cancellationToken)
        {
            return Task.FromResult(Handle(request));
        }

        public GetAllResult<Group> Handle(Request request)
        {
            var accountResult = AccountService.FindById(request.Id);

            if (accountResult.NotFound)
                return new NotFound();

            var account = accountResult.AsFound;

            var result = GroupService.FindAll(
                x => x.Accounts.Any(a => a.Id == account.Id));

            return result;
        }
    }
}