using Yuque.Application.Repositories.Dtos;

namespace Yuque.Application.Repositories.Services;

public interface IRepositoryQueryService
{
    Task<IReadOnlyList<RepositoryDto>> ListAsync(CancellationToken cancellationToken = default);
}
