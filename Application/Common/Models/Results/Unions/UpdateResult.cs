using OneOf;

namespace Application.Common.Models.Results.Unions;

[GenerateOneOf]
public partial class UpdateResult<T> : OneOfBase<T, NotFound, ValidationFailed, Failed>
{
}
