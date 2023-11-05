using OneOf;

namespace Application.Common.Models.Results.Unions;

[GenerateOneOf]
public partial class GetResult<T> : OneOfBase<T, NotFound>
{
}
