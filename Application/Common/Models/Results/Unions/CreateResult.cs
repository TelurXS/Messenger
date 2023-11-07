using OneOf;

namespace Application.Common.Models.Results.Unions;

[GenerateOneOf]
public partial class CreateResult<T> : OneOfBase<T, ValidationFailed, Failed>
{
    public bool Success => IsT0;
    public bool ValidationFailed => IsT1;
    public bool Failed => IsT2;
}
