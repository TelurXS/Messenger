using OneOf;

namespace Application.Common.Models.Results.Unions;

[GenerateOneOf]
public partial class CreateResult<T> : OneOfBase<T, ValidationFailed, Failed>
{
}
