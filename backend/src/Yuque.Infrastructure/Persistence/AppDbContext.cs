using Microsoft.EntityFrameworkCore;
using Yuque.Domain.Common.Entities;
using Yuque.Domain.Documents.Entities;
using Yuque.Domain.Repositories.Entities;

namespace Yuque.Infrastructure.Persistence;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
	public DbSet<Repository> Repositories => Set<Repository>();

	public DbSet<Document> Documents => Set<Document>();

	public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
	{
		ApplyAuditInfo();
		return await base.SaveChangesAsync(cancellationToken);
	}

	public override int SaveChanges()
	{
		ApplyAuditInfo();
		return base.SaveChanges();
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
		base.OnModelCreating(modelBuilder);
	}

	private void ApplyAuditInfo()
	{
		var now = DateTimeOffset.UtcNow;

		foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
		{
			if (entry.State == EntityState.Added)
			{
				entry.Entity.CreatedAt = now;
				entry.Entity.UpdatedAt = now;
			}

			if (entry.State == EntityState.Modified)
			{
				entry.Entity.UpdatedAt = now;
			}
		}
	}
}
