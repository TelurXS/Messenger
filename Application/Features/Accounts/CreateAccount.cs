using Application.Common.Interfaces.Mappings;
using Application.Common.Interfaces.Services;
using Application.Common.Models.Results;
using Application.Common.Models.Results.Unions;
using Application.Entities;
using FluentValidation;
using MediatR;

namespace Application.Features.Accounts;

public static class CreateAccount
{
    public class Request : IRequest<CreateResult<Account>> 
    {
        public string Name { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Login { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;
    }

    public class Validator : AbstractValidator<Request>
    {
        public Validator(IAccountService service)
        {
            RuleFor(x => x.Name)
                .MaximumLength(32)
                .NotEmpty();

            RuleFor(x => x.Login)
                .NotEmpty()
                .MaximumLength(32)
                .Must((request, x) => service.IsLoginUnique(x));


            RuleFor(x => x.Email)
                .NotEmpty()
                .MaximumLength(64)
                .EmailAddress()
                .Must((request, x) => service.IsEmailUnique(x));


            RuleFor(x => x.Password)
                .NotEmpty()
                .MaximumLength(256);
        }
    }

    public class Handler : IRequestHandler<Request, CreateResult<Account>>
    {
        public Handler(IAccountService accountService, IAccountMapper mapper, IValidator<Request> validator)
        {
            AccountService = accountService;
            Mapper = mapper;
            Validator = validator;
        }

        private IAccountService AccountService { get; }
        private IAccountMapper Mapper { get; }
        private IValidator<Request> Validator { get; }

        public Task<CreateResult<Account>> Handle(Request request, CancellationToken cancellationToken)
        {
            return Task.FromResult(Handle(request));
        }

        public CreateResult<Account> Handle(Request request)
        {
            var validationResult = Validator.Validate(request);

            if (validationResult.IsValid is false)
                return new ValidationFailed(validationResult.Errors);

            var account = Mapper.FromRequest(request);

            var result = AccountService.Create(account);

            return result;
        }
    }
}
