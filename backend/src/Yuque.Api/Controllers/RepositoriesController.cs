using Microsoft.AspNetCore.Mvc;
using Yuque.Api.Models.Requests.Repositories;
using Yuque.Application.Repositories.Commands;
using Yuque.Application.Repositories.Services;

namespace Yuque.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RepositoriesController(IRepositoryAppService repositoryAppService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> ListAsync(CancellationToken cancellationToken)
    {
        var repositories = await repositoryAppService.ListAsync(cancellationToken);
        return Ok(repositories);
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync(
        [FromBody] CreateRepositoryRequest request,
        CancellationToken cancellationToken)
    {
        var repository = await repositoryAppService.CreateAsync(new CreateRepositoryCommand
        {
            Name = request.Name,
            Slug = request.Slug,
            Visibility = request.Visibility,
        }, cancellationToken);

        return CreatedAtAction(nameof(ListAsync), new { id = repository.Id }, repository);
    }
}
