using Application.Common.Interfaces.Services;
using Application.Common.Models.Results.Unions;
using Application.Entities;
using MediatR;

namespace Application.Features.Accounts;

public static partial class GetAccountById
{
    public class Request : IRequest<GetResult<Account>>
    {
        public int Id { get; set; }
    }

    public class Handler : IRequestHandler<Request, GetResult<Account>>
    {
        public Handler(IAccountService accountService)
        {
            AccountService = accountService;
        }

        private IAccountService AccountService { get; }

        public Task<GetResult<Account>> Handle(Request request, CancellationToken cancellationToken)
        {
            return Task.FromResult(Handle(request));
        }

        public GetResult<Account> Handle(Request request)
        {
            var result = AccountService.FindById(request.Id);

            return result;
        }
    }
}