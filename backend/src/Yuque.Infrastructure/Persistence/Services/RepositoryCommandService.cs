using Microsoft.EntityFrameworkCore;
using Yuque.Application.Repositories.Commands;
using Yuque.Application.Repositories.Dtos;
using Yuque.Application.Repositories.Services;
using Yuque.Domain.Repositories.Entities;

namespace Yuque.Infrastructure.Persistence.Services;

public class RepositoryCommandService(AppDbContext dbContext) : IRepositoryCommandService
{
    public async Task<RepositoryDto> CreateAsync(CreateRepositoryCommand command, CancellationToken cancellationToken = default)
    {
        var normalizedName = command.Name.Trim();
        var slug = BuildSlug(command.Slug, normalizedName);

        var exists = await dbContext.Repositories.AnyAsync(entity => entity.Slug == slug, cancellationToken);
        if (exists)
        {
            throw new InvalidOperationException($"Repository slug '{slug}' already exists.");
        }

        var repository = new Repository
        {
            Name = normalizedName,
            Slug = slug,
            Visibility = command.Visibility,
            Status = "active",
        };

        dbContext.Repositories.Add(repository);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new RepositoryDto
        {
            Id = repository.Id,
            Name = repository.Name,
            Slug = repository.Slug,
            Visibility = repository.Visibility,
            UpdatedAt = repository.UpdatedAt,
        };
    }

    private static string BuildSlug(string? slug, string name)
    {
        var source = string.IsNullOrWhiteSpace(slug) ? name : slug;

        return source.Trim()
            .ToLowerInvariant()
            .Replace(" ", "-");
    }
}
