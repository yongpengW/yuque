using Yuque.Domain.Common.Entities;

namespace Yuque.Domain.Repositories.Entities;

public class Repository : AuditableEntity
{
    public string Name { get; set; } = string.Empty;

    public string Slug { get; set; } = string.Empty;

    public string Visibility { get; set; } = "private";

    public string Status { get; set; } = "active";
}
