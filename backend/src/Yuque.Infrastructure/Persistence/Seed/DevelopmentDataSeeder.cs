using Microsoft.EntityFrameworkCore;
using Yuque.Domain.Documents.Entities;
using Yuque.Domain.Repositories.Entities;

namespace Yuque.Infrastructure.Persistence.Seed;

public class DevelopmentDataSeeder(AppDbContext dbContext)
{
    public async Task SeedAsync(CancellationToken cancellationToken = default)
    {
        if (await dbContext.Repositories.AnyAsync(cancellationToken))
        {
            return;
        }

        var repository = new Repository
        {
            Name = "产品知识库",
            Slug = "product-docs",
            Visibility = "private",
            Status = "active",
        };

        dbContext.Repositories.Add(repository);
        await dbContext.SaveChangesAsync(cancellationToken);

        var documents = new[]
        {
            new Document
            {
                RepositoryId = repository.Id,
                Title = "欢迎使用知识库",
                Status = "published",
            },
            new Document
            {
                RepositoryId = repository.Id,
                Title = "文档协作规范",
                Status = "draft",
            },
        };

        dbContext.Documents.AddRange(documents);
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}
