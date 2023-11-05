using OneOf;

namespace Application.Common.Models.Results.Unions;

[GenerateOneOf]
public partial class DeleteResult : OneOfBase<Success, NotFound, Failed>
{
}
