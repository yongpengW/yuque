namespace Yuque.Domain.Common.Entities;

public abstract class AuditableEntity
{
    public long Id { get; set; }

    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;

    public DateTimeOffset UpdatedAt { get; set; } = DateTimeOffset.UtcNow;
}
