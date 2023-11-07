using Application.Common.Interfaces.Services;
using Application.Common.Models.Results.Unions;
using MediatR;

namespace Application.Features.Accounts;

public static class DeleteAccountById
{
    public class Request : IRequest<DeleteResult>
    {
        public int Id { get; set; }
    }
    
    public class Handler : IRequestHandler<Request, DeleteResult>
    {
        
        public Handler(IAccountService accountService)
        {
            AccountService = accountService;
        }
        
        public IAccountService AccountService { get; }
        
        public Task<DeleteResult> Handle(Request request, CancellationToken cancellationToken)
        {
            return Task.FromResult(Handle(request));
        }

        public DeleteResult Handle(Request request)
        {
            var result = AccountService.Delete(request.Id);

            return result;
        }
    }
}