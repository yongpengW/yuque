using FluentValidation;

namespace Yuque.Application.Documents.Commands;

public class CreateDocumentCommandValidator : AbstractValidator<CreateDocumentCommand>
{
    public CreateDocumentCommandValidator()
    {
        RuleFor(command => command.RepositoryId)
            .GreaterThan(0);

        RuleFor(command => command.Title)
            .NotEmpty()
            .MaximumLength(255);
    }
}
