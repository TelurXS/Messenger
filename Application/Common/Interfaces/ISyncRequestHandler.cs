using MediatR;

namespace Application.Common.Interfaces;

public interface ISyncRequestHandler<in TRequest, TResponse> 
    : IRequestHandler<TRequest, TResponse> 
    where TRequest : IRequest<TResponse>
{
    new Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken)
    {
        return Task.FromResult(Handle(request));
    }
    
    TResponse Handle(TRequest request);
}