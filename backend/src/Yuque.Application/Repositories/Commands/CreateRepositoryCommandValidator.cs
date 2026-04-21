using FluentValidation;

namespace Yuque.Application.Repositories.Commands;

public class CreateRepositoryCommandValidator : AbstractValidator<CreateRepositoryCommand>
{
    public CreateRepositoryCommandValidator()
    {
        RuleFor(command => command.Name)
            .NotEmpty()
            .MaximumLength(150);

        RuleFor(command => command.Slug)
            .MaximumLength(150)
            .Matches("^[a-zA-Z0-9-]*$")
            .When(command => !string.IsNullOrWhiteSpace(command.Slug));

        RuleFor(command => command.Visibility)
            .Must(visibility => visibility is "private" or "team" or "public")
            .WithMessage("Visibility must be one of: private, team, public.");
    }
}
