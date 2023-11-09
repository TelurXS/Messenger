using OneOf;

namespace Application.Common.Models.Results.Unions;

[GenerateOneOf]
public partial class GetAllResult<T> : OneOfBase<List<T>, NotFound>
{
    public bool Found => IsT0;
    public bool NotFound => IsT1;
    
    public List<T> AsFound => AsT0;
    public NotFound AsNotFound => AsT1;
}
