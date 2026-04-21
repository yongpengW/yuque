namespace Yuque.Application.Repositories.Commands;

public class CreateRepositoryCommand
{
    public string Name { get; init; } = string.Empty;

    public string? Slug { get; init; }

    public string Visibility { get; init; } = "private";
}
