using FluentValidation;
using Yuque.Application.Repositories.Commands;
using Yuque.Application.Repositories.Dtos;

namespace Yuque.Application.Repositories.Services;

public class RepositoryAppService(
    IRepositoryQueryService repositoryQueryService,
    IValidator<CreateRepositoryCommand> createRepositoryCommandValidator,
    IRepositoryCommandService repositoryCommandService) : IRepositoryAppService
{
    public Task<IReadOnlyList<RepositoryDto>> ListAsync(CancellationToken cancellationToken = default)
    {
        return repositoryQueryService.ListAsync(cancellationToken);
    }

    public async Task<RepositoryDto> CreateAsync(CreateRepositoryCommand command, CancellationToken cancellationToken = default)
    {
        await createRepositoryCommandValidator.ValidateAndThrowAsync(command, cancellationToken);
        return await repositoryCommandService.CreateAsync(command, cancellationToken);
    }
}
