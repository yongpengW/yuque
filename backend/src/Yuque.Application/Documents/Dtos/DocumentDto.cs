namespace Yuque.Application.Documents.Dtos;

public class DocumentDto
{
    public long Id { get; init; }

    public long RepositoryId { get; init; }

    public string Title { get; init; } = string.Empty;

    public string Status { get; init; } = string.Empty;

    public DateTimeOffset UpdatedAt { get; init; }
}
