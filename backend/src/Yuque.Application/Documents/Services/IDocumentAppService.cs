using Yuque.Application.Documents.Commands;
using Yuque.Application.Documents.Dtos;

namespace Yuque.Application.Documents.Services;

public interface IDocumentAppService
{
    Task<IReadOnlyList<DocumentDto>> ListAsync(CancellationToken cancellationToken = default);

    Task<DocumentDto> CreateAsync(CreateDocumentCommand command, CancellationToken cancellationToken = default);
}
