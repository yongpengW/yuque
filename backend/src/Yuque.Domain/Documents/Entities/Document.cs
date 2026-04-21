using Yuque.Domain.Common.Entities;

namespace Yuque.Domain.Documents.Entities;

public class Document : AuditableEntity
{
    public long RepositoryId { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Status { get; set; } = "draft";
}
