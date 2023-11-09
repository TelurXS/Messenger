using Application.Common.Interfaces;
using Application.Common.Interfaces.Services;
using Application.Common.Models.Results.Unions;
using Application.Entities;
using MediatR;

namespace Application.Features.Accounts;

public static class GetAllAccounts
{
    public class Request : IRequest<GetAllResult<Account>>
    {
    }
    
    public class Handler : ISyncRequestHandler<Request, GetAllResult<Account>>
    {
        public Handler(IAccountService accountService)
        {
            AccountService = accountService;
        }
        
        private IAccountService AccountService { get; }

        public Task<GetAllResult<Account>> Handle(Request request, CancellationToken cancellationToken)
        {
            return Task.FromResult(Handle(request));
        }

        public GetAllResult<Account> Handle(Request request)
        {
            var result = AccountService.FindAll();

            return result;
        }
    }
}