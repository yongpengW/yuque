using Microsoft.EntityFrameworkCore;
using Yuque.Application.Documents.Commands;
using Yuque.Application.Documents.Dtos;
using Yuque.Application.Documents.Services;
using Yuque.Domain.Documents.Entities;

namespace Yuque.Infrastructure.Persistence.Services;

public class DocumentCommandService(AppDbContext dbContext) : IDocumentCommandService
{
    public async Task<DocumentDto> CreateAsync(CreateDocumentCommand command, CancellationToken cancellationToken = default)
    {
        var repositoryExists = await dbContext.Repositories
            .AnyAsync(entity => entity.Id == command.RepositoryId, cancellationToken);

        if (!repositoryExists)
        {
            throw new InvalidOperationException($"Repository '{command.RepositoryId}' does not exist.");
        }

        var document = new Document
        {
            RepositoryId = command.RepositoryId,
            Title = command.Title.Trim(),
            Status = "draft",
        };

        dbContext.Documents.Add(document);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new DocumentDto
        {
            Id = document.Id,
            RepositoryId = document.RepositoryId,
            Title = document.Title,
            Status = document.Status,
            UpdatedAt = document.UpdatedAt,
        };
    }
}
