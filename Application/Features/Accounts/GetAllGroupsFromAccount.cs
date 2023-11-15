using Application.Common.Interfaces;
using Application.Common.Interfaces.Services;
using Application.Common.Models.Results;
using Application.Common.Models.Results.Unions;
using Application.Entities;
using MediatR;

namespace Application.Features.Accounts;

public static class GetAllGroupsFromAccount
{
    public class Request : IRequest<GetAllResult<Group>>
    {
        public int Id { get; set; }
    }
    
    public class Handler : ISyncRequestHandler<Request, GetAllResult<Group>>
    {
        public Handler(IAccountService accountService)
        {
            AccountService = accountService;
        }
        
        private IAccountService AccountService { get; }
        
        public Task<GetAllResult<Group>> Handle(Request request, CancellationToken cancellationToken)
        {
            return Task.FromResult(Handle(request));
        }

        public GetAllResult<Group> Handle(Request request)
        {
            var result = AccountService.FindById(request.Id);

            if (result.NotFound)
                return new NotFound();

            return result.AsFound.Groups;
        }
    }
}