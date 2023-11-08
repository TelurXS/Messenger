using Application.Common.Interfaces;
using Application.Common.Interfaces.Mappings;
using Application.Common.Interfaces.Services;
using Application.Common.Models.Results;
using Application.Common.Models.Results.Unions;
using Application.Entities;
using FluentValidation;
using MediatR;

namespace Application.Features.Groups;

public static class UpdateGroup
{
    public class Request : IRequest<UpdateResult<Group>>
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;
    }
    
    public class Validator : AbstractValidator<Request>
    {
        public Validator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(64);

            RuleFor(x => x.Description)
                .MaximumLength(256);
        }
    }
    
    public class Handler : ISyncRequestHandler<Request, UpdateResult<Group>>
    {
        public Handler(IGroupService groupService, IGroupMapper mapper, IValidator<Request> validator)
        {
            GroupService = groupService;
            Mapper = mapper;
            Validator = validator;
        }

        private IGroupService GroupService { get; }
        private IGroupMapper Mapper { get; } 
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

            var group = Mapper.FromRequest(request);

            var result = GroupService.Update(request.Id, group);

            return result;
        }
    }
}