using OneOf;

namespace Application.Common.Models.Results.Unions;

[GenerateOneOf]
public partial class UpdateResult<T> : OneOfBase<T, NotFound, ValidationFailed, Failed>
{
    public bool Success => IsT0;
    public bool NotFound => IsT1;
    public bool ValidationFailed => IsT2;
    public bool Failed => IsT3;
}
