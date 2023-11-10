using Application.Common.Interfaces;
using Application.Common.Interfaces.Mappings;
using Application.Common.Interfaces.Services;
using Application.Common.Models.Messages;
using Application.Common.Models.Results;
using Application.Common.Models.Results.Unions;
using Application.Entities;
using FluentValidation;
using MediatR;

namespace Application.Features.Accounts;

public static class UpdateAccount
{
    public class Request : IRequest<UpdateResult<Account>>
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Login { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;
    }

    public class Validator : AbstractValidator<Request> 
    {
        public Validator(IAccountService service)
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage(TranslatableMessages.Validation.PROPERTY_CANNOT_BE_EMPTY)
                .Must(x => service.FindById(x).Found)
                .WithMessage(TranslatableMessages.Validation.PROPERTY_MUST_BE_CORRECT_LENGTH);

            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage(TranslatableMessages.Validation.PROPERTY_CANNOT_BE_EMPTY)
                .MaximumLength(32)
                .WithMessage(TranslatableMessages.Validation.PROPERTY_MUST_BE_CORRECT_LENGTH);

            RuleFor(x => x.Login)
                .NotEmpty()
                .WithMessage(TranslatableMessages.Validation.PROPERTY_CANNOT_BE_EMPTY)
                .MaximumLength(32)
                .WithMessage(TranslatableMessages.Validation.PROPERTY_MUST_BE_CORRECT_LENGTH)
                .Must((request, x) => service.IsLoginAvailableForId(request.Id, x))
                .WithMessage(TranslatableMessages.Validation.Accounts.LOGIN_IS_NOT_AVAILABLE);


            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage(TranslatableMessages.Validation.PROPERTY_CANNOT_BE_EMPTY)
                .MaximumLength(64)
                .WithMessage(TranslatableMessages.Validation.PROPERTY_MUST_BE_CORRECT_LENGTH)
                .EmailAddress()
                .WithMessage(TranslatableMessages.Validation.PROPERTY_MUST_BE_EMAIL)
                .Must((request, x) => service.IsEmailAvailableForId(request.Id, x))
                .WithMessage(TranslatableMessages.Validation.Accounts.EMAIL_IS_NOT_AVAILABLE);

            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage(TranslatableMessages.Validation.PROPERTY_CANNOT_BE_EMPTY)
                .MaximumLength(256)
                .WithMessage(TranslatableMessages.Validation.PROPERTY_MUST_BE_CORRECT_LENGTH);
        }
    }

    public class Handler : ISyncRequestHandler<Request, UpdateResult<Account>>
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

        public Task<UpdateResult<Account>> Handle(Request request, CancellationToken cancellationToken)
        {
            return Task.FromResult(Handle(request));
        }

        public UpdateResult<Account> Handle(Request request)
        {
            var validationResult = Validator.Validate(request);

            if (validationResult.IsValid is false)
                return new ValidationFailed(validationResult.Errors);

            var account = Mapper.FromRequest(request);

            var result = AccountService.Update(request.Id, account);

            return result;
        }
    }
}
