using Yuque.Application.Repositories.Commands;
using Yuque.Application.Repositories.Dtos;

namespace Yuque.Application.Repositories.Services;

public interface IRepositoryCommandService
{
    Task<RepositoryDto> CreateAsync(CreateRepositoryCommand command, CancellationToken cancellationToken = default);
}
