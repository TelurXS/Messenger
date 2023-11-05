using OneOf;

namespace Application.Common.Models.Results.Unions;

[GenerateOneOf]
public partial class GetAllResult<T> : OneOfBase<List<T>, NotFound>
{
}
