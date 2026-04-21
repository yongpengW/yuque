using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Yuque.Domain.Repositories.Entities;

namespace Yuque.Infrastructure.Persistence.Configurations.Repositories;

public class RepositoryConfiguration : IEntityTypeConfiguration<Repository>
{
    public void Configure(EntityTypeBuilder<Repository> builder)
    {
        builder.ToTable("repositories");

        builder.HasKey(entity => entity.Id);
        builder.Property(entity => entity.Id).ValueGeneratedOnAdd();

        builder.Property(entity => entity.Name).HasMaxLength(150).IsRequired();
        builder.Property(entity => entity.Slug).HasMaxLength(150).IsRequired();
        builder.Property(entity => entity.Visibility).HasMaxLength(20).IsRequired();
        builder.Property(entity => entity.Status).HasMaxLength(20).IsRequired();

        builder.HasIndex(entity => entity.Slug).IsUnique();
    }
}
