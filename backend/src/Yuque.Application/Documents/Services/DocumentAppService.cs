using FluentValidation;
using Yuque.Application.Documents.Commands;
using Yuque.Application.Documents.Dtos;

namespace Yuque.Application.Documents.Services;

public class DocumentAppService(
    IDocumentQueryService documentQueryService,
    IValidator<CreateDocumentCommand> createDocumentCommandValidator,
    IDocumentCommandService documentCommandService) : IDocumentAppService
{
    public Task<IReadOnlyList<DocumentDto>> ListAsync(CancellationToken cancellationToken = default)
    {
        return documentQueryService.ListAsync(cancellationToken);
    }

    public async Task<DocumentDto> CreateAsync(CreateDocumentCommand command, CancellationToken cancellationToken = default)
    {
        await createDocumentCommandValidator.ValidateAndThrowAsync(command, cancellationToken);
        return await documentCommandService.CreateAsync(command, cancellationToken);
    }
}
