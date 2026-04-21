namespace Yuque.Application.Documents.Commands;

public class CreateDocumentCommand
{
    public long RepositoryId { get; init; }

    public string Title { get; init; } = string.Empty;
}
