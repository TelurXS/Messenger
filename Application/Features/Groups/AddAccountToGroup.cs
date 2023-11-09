using Application.Common.Interfaces;
using Application.Common.Interfaces.Services;
using Application.Common.Models.Results;
using Application.Common.Models.Results.Unions;
using Application.Entities;
using FluentValidation;
using MediatR;

namespace Application.Features.Groups;

public static class AddAccountToGroup
{
    public class Request : IRequest<UpdateResult<Group>>
    {
        public int AccountId { get; set; }
        public int GroupId { get; set; }
    }

    public class Validator : AbstractValidator<Request>
    {
        public Validator(IAccountService accountService, IGroupService groupService)
        {
            RuleFor(x => x.AccountId)
                .Must(x => accountService.FindById(x).Found);
            
            RuleFor(x => x.GroupId)
                .Must(x => groupService.FindById(x).Found);
        }
    }

    public class Handler : ISyncRequestHandler<Request, UpdateResult<Group>>
    {
        public Handler(
            IAccountService accountService,
            IGroupService groupService,
            IValidator<Request> validator)
        {
            AccountService = accountService;
            GroupService = groupService;
            Validator = validator;
        }
        
        private IAccountService AccountService { get; }
        private IGroupService GroupService { get; }
        private IValidator<Request> Validator { get; }

        public Task<UpdateResult<Group>> Handle(Request request, CancellationToken cancellationToken)
        {
            return Task.FromResult(Handle(request));
        }

        public UpdateResult<Group> Handle(Request request)
        {
            var validationResult = Validator.Validate(request);

            if (validationResult.IsValid is false)
                return new ValidationFailed(validationResult.Errors);

            var account = AccountService.FindById(request.AccountId).AsFound;
            var group = GroupService.FindById(request.GroupId).AsFound;

            var result = GroupService.AddAccountToGroup(account, group);

            return result;
        }
    }
}