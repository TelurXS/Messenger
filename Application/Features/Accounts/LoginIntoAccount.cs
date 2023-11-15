using Application.Common.Interfaces;
using Application.Common.Interfaces.Services;
using Application.Common.Models.Messages;
using Application.Common.Models.Results;
using Application.Common.Models.Results.Unions;
using FluentValidation;
using MediatR;

namespace Application.Features.Accounts;

public static class LoginIntoAccount
{
    public class Request : IRequest<LoginResult>
    {
        public string Login { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;
    }
    
    public class Validator : AbstractValidator<Request>
    {
        public Validator(IAccountService service)
        {
            ClassLevelCascadeMode = CascadeMode.Stop;
            RuleLevelCascadeMode = CascadeMode.Stop;
            
            RuleFor(x => x.Login)
                .NotEmpty()
                .WithMessage(TranslatableMessages.Validation.PROPERTY_CANNOT_BE_EMPTY)
                .MaximumLength(32)
                .WithMessage(TranslatableMessages.Validation.PROPERTY_MUST_BE_CORRECT_LENGTH)
                .Must((request, x) => service.FindByLogin(x).Found)
                .WithMessage(TranslatableMessages.Validation.Accounts.LOGIN_IS_NOT_EXIST);

            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage(TranslatableMessages.Validation.PROPERTY_CANNOT_BE_EMPTY)
                .MaximumLength(256)
                .WithMessage(TranslatableMessages.Validation.PROPERTY_MUST_BE_CORRECT_LENGTH)
                .Must((request, x) => service.FindByLogin(request.Login).AsFound.Password.Equals(x))
                .WithMessage(TranslatableMessages.Validation.Accounts.INCORRECT_PASSWORD);
        }
    }
    
    public class Handler : ISyncRequestHandler<Request, LoginResult>
    {
        public Handler(IAccountService accountService, IValidator<Request> validator)
        {
            AccountService = accountService;
            Validator = validator;
        }
        
        private IAccountService AccountService { get; }
        private IValidator<Request> Validator { get; }
        
        public Task<LoginResult> Handle(Request request, CancellationToken cancellationToken)
        {
            return Task.FromResult(Handle(request));
        }

        public LoginResult Handle(Request request)
        {
            var validationResult = Validator.Validate(request);

            if (validationResult.IsValid is false)
                return new ValidationFailed(validationResult.Errors);
            
            var result = AccountService.FindByLogin(request.Login);

            if (result.NotFound)
                return new NotFound();

            return result.AsFound;
        }
    }
}