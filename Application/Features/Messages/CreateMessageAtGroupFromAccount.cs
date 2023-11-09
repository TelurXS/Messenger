using Application.Common.Interfaces;
using Application.Common.Interfaces.Mappings;
using Application.Common.Interfaces.Services;
using Application.Common.Models.Results;
using Application.Common.Models.Results.Unions;
using Application.Entities;
using FluentValidation;
using MediatR;

namespace Application.Features.Messages;

public static class CreateMessageAtGroupFromAccount
{
    public class Request : IRequest<CreateResult<Message>>
    {
        public int GroupId { get; set; }
        
        public int AccountId { get; set; }

        public string Content { get; set; } = string.Empty;
        
        public DateTime SentAt { get; set; } = default;
    }
    
    public class Validator : AbstractValidator<Request>
    {
        public Validator(IGroupService groupService, IAccountService accountService)
        {
            RuleFor(x => x.GroupId)
                .Must((request, x) => groupService.FindById(request.GroupId).Found);
            
            RuleFor(x => x.AccountId)
                .Must((request, x) => accountService.FindById(request.AccountId).Found);

            RuleFor(x => x.Content)
                .NotEmpty()
                .MaximumLength(128);

            RuleFor(x => x.SentAt)
                .Must(x => x <= DateTime.Now);
        }
    }
    
    public class Handler : ISyncRequestHandler<Request, CreateResult<Message>>
    {
        public Handler(
            IMessageService messageService, 
            IAccountService accountService, 
            IGroupService groupService,
            IValidator<Request> validator,
            IMessageMapper mapper)
        {
            MessageService = messageService;
            AccountService = accountService;
            GroupService = groupService;
            Validator = validator;
            Mapper = mapper;
        }
        
        private IMessageService MessageService { get; }
        private IAccountService AccountService { get; }
        private IGroupService GroupService { get; }
        private IValidator<Request> Validator { get; }
        private IMessageMapper Mapper { get; }

        public Task<CreateResult<Message>> Handle(Request request, CancellationToken cancellationToken)
        {
            return Task.FromResult(Handle(request));
        }

        public CreateResult<Message> Handle(Request request)
        {
            var validationResult = Validator.Validate(request);

            if (validationResult.IsValid is false)
                return new ValidationFailed(validationResult.Errors);

            var account = AccountService.FindById(request.AccountId).AsFound;

            var group = GroupService.FindById(request.GroupId).AsFound;

            if (group.Accounts.Contains(account) is false)
                return new Failed();
            
            var message = Mapper.FromRequest(request);
            message.Group = group;
            message.Sender = account;

            var result = MessageService.Create(message);

            return result;
        }
    }
}