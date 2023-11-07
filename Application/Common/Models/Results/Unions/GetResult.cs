using OneOf;

namespace Application.Common.Models.Results.Unions;

[GenerateOneOf]
public partial class GetResult<T> : OneOfBase<T, NotFound>
{
    public bool Found => IsT0;
    public bool NotFound => IsT1;
}
