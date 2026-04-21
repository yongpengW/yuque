using Yuque.Application.Repositories.Commands;
using Yuque.Application.Repositories.Dtos;

namespace Yuque.Application.Repositories.Services;

public interface IRepositoryAppService
{
    Task<IReadOnlyList<RepositoryDto>> ListAsync(CancellationToken cancellationToken = default);

    Task<RepositoryDto> CreateAsync(CreateRepositoryCommand command, CancellationToken cancellationToken = default);
}
