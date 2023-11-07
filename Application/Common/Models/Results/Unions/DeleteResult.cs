using OneOf;

namespace Application.Common.Models.Results.Unions;

[GenerateOneOf]
public partial class DeleteResult : OneOfBase<Success, NotFound, Failed>
{
    public bool Success => IsT0;
    public bool NotFound => IsT1;
    public bool Failed => IsT2;
}
