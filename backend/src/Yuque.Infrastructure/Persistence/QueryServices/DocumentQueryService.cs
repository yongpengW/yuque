using Microsoft.EntityFrameworkCore;
using Yuque.Application.Documents.Dtos;
using Yuque.Application.Documents.Services;

namespace Yuque.Infrastructure.Persistence.QueryServices;

public class DocumentQueryService(AppDbContext dbContext) : IDocumentQueryService
{
    public async Task<IReadOnlyList<DocumentDto>> ListAsync(CancellationToken cancellationToken = default)
    {
        return await dbContext.Documents
            .AsNoTracking()
            .OrderByDescending(entity => entity.UpdatedAt)
            .Select(entity => new DocumentDto
            {
                Id = entity.Id,
                RepositoryId = entity.RepositoryId,
                Title = entity.Title,
                Status = entity.Status,
                UpdatedAt = entity.UpdatedAt,
            })
            .ToListAsync(cancellationToken);
    }
}
