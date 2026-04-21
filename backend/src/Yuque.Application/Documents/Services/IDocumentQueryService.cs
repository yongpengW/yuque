using Yuque.Application.Documents.Dtos;

namespace Yuque.Application.Documents.Services;

public interface IDocumentQueryService
{
    Task<IReadOnlyList<DocumentDto>> ListAsync(CancellationToken cancellationToken = default);
}
