namespace Yuque.Api.Models.Requests.Documents;

public class CreateDocumentRequest
{
    public long RepositoryId { get; init; }

    public string Title { get; init; } = string.Empty;
}
