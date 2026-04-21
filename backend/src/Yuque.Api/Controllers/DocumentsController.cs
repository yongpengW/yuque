using Microsoft.AspNetCore.Mvc;
using Yuque.Api.Models.Requests.Documents;
using Yuque.Application.Documents.Commands;
using Yuque.Application.Documents.Services;

namespace Yuque.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DocumentsController(IDocumentAppService documentAppService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> ListAsync(CancellationToken cancellationToken)
    {
        var documents = await documentAppService.ListAsync(cancellationToken);
        return Ok(documents);
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync(
        [FromBody] CreateDocumentRequest request,
        CancellationToken cancellationToken)
    {
        var document = await documentAppService.CreateAsync(new CreateDocumentCommand
        {
            RepositoryId = request.RepositoryId,
            Title = request.Title,
        }, cancellationToken);

        return CreatedAtAction(nameof(ListAsync), new { id = document.Id }, document);
    }
}
