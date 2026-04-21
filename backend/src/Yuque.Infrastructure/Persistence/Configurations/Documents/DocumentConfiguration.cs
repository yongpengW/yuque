using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Yuque.Domain.Documents.Entities;
using Yuque.Domain.Repositories.Entities;

namespace Yuque.Infrastructure.Persistence.Configurations.Documents;

public class DocumentConfiguration : IEntityTypeConfiguration<Document>
{
    public void Configure(EntityTypeBuilder<Document> builder)
    {
        builder.ToTable("documents");

        builder.HasKey(entity => entity.Id);
        builder.Property(entity => entity.Id).ValueGeneratedOnAdd();

        builder.Property(entity => entity.Title).HasMaxLength(255).IsRequired();
        builder.Property(entity => entity.Status).HasMaxLength(20).IsRequired();

        builder.HasIndex(entity => entity.RepositoryId);

        builder.HasOne<Repository>()
            .WithMany()
            .HasForeignKey(entity => entity.RepositoryId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
