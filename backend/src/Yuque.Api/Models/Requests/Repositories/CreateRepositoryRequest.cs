namespace Yuque.Api.Models.Requests.Repositories;

public class CreateRepositoryRequest
{
    public string Name { get; init; } = string.Empty;

    public string? Slug { get; init; }

    public string Visibility { get; init; } = "private";
}
