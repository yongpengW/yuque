using Yuque.Application.Documents.Commands;
using Yuque.Application.Documents.Dtos;

namespace Yuque.Application.Documents.Services;

public interface IDocumentCommandService
{
    Task<DocumentDto> CreateAsync(CreateDocumentCommand command, CancellationToken cancellationToken = default);
}
