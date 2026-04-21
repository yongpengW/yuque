namespace Yuque.Application.Repositories.Dtos;

public class RepositoryDto
{
    public long Id { get; init; }

    public string Name { get; init; } = string.Empty;

    public string Slug { get; init; } = string.Empty;

    public string Visibility { get; init; } = string.Empty;

    public DateTimeOffset UpdatedAt { get; init; }
}
