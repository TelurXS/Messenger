using FluentValidation.Results;

namespace Application.Common.Models.Results;

public class ValidationFailed
{
    public ValidationFailed(IEnumerable<ValidationFailure> errors)
    {
        Errors = errors;
    }

    public ValidationFailed(ValidationFailure error) : this(new[] { error })
    {
    }

    public IEnumerable<ValidationFailure> Errors { get; }
}
