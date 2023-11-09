using Application.Common.Interfaces;
using Application.Common.Interfaces.Mappings;
using Application.Common.Interfaces.Services;
using Application.Common.Models.Results;
using Application.Common.Models.Results.Unions;
using Application.Entities;
using FluentValidation;
using MediatR;

namespace Application.Features.Messages;

public static class UpdateMessage
{
    public class Request : IRequest<UpdateResult<Message>>
    {
        public int Id { get; set; }
        
        public string Content { get; set; } = string.Empty;

        public DateTime SentAt { get; set; } = default;
    }
    
    public class Validator : AbstractValidator<Request>
    {
        public Validator(IGroupService groupService)
        {
            RuleFor(x => x.Id)
                .Must(x => groupService.FindById(x).Found);
            
            RuleFor(x => x.Content)
                .NotEmpty()
                .MaximumLength(128);

            RuleFor(x => x.SentAt)
                .Must(x => x <= DateTime.Now);
        }
    }
    
    public class Handler : ISyncRequestHandler<Request, UpdateResult<Message>>
    {
        public Handler(
            IMessageService messageService, 
            IMessageMapper mapper,
            IValidator<Request> validator)
        {
            MessageService = messageService;
            Mapper = mapper;
            Validator = validator;
        }
        
        private IMessageService MessageService { get; }
        private IMessageMapper Mapper { get; }
        private IValidator<Request> Validator { get; }

        public Task<UpdateResult<Message>> Handle(Request request, CancellationToken cancellationToken)
        {
            return Task.FromResult(Handle(request));
        }

        public UpdateResult<Message> Handle(Request request)
        {
            var validationResult = Validator.Validate(request);

            if (validationResult.IsValid is false)
                return new ValidationFailed(validationResult.Errors);

            var message = Mapper.FromRequest(request);

            var result = MessageService.Update(request.Id, message);

            return result;
        }
    }
}