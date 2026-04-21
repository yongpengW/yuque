using Microsoft.EntityFrameworkCore;
using Yuque.Application.Repositories.Dtos;
using Yuque.Application.Repositories.Services;

namespace Yuque.Infrastructure.Persistence.QueryServices;

public class RepositoryQueryService(AppDbContext dbContext) : IRepositoryQueryService
{
    public async Task<IReadOnlyList<RepositoryDto>> ListAsync(CancellationToken cancellationToken = default)
    {
        return await dbContext.Repositories
            .AsNoTracking()
            .OrderByDescending(entity => entity.UpdatedAt)
            .Select(entity => new RepositoryDto
            {
                Id = entity.Id,
                Name = entity.Name,
                Slug = entity.Slug,
                Visibility = entity.Visibility,
                UpdatedAt = entity.UpdatedAt,
            })
            .ToListAsync(cancellationToken);
    }
}
