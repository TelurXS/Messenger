using Application.Common.Interfaces.Persistance;

namespace Application.Infrastructure.Services;

public class RepositoryService<T, TRepository>
    where TRepository : IRepository<T>
{
	public RepositoryService(TRepository repository)
	{
		Repository = repository;
	}

	protected TRepository Repository { get; }
}
