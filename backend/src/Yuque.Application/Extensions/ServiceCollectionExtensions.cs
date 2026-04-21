using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Yuque.Application.Documents.Commands;
using Yuque.Application.Documents.Services;
using Yuque.Application.Repositories.Commands;
using Yuque.Application.Repositories.Services;

namespace Yuque.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IValidator<CreateRepositoryCommand>, CreateRepositoryCommandValidator>();
        services.AddScoped<IValidator<CreateDocumentCommand>, CreateDocumentCommandValidator>();
        services.AddScoped<IRepositoryAppService, RepositoryAppService>();
        services.AddScoped<IDocumentAppService, DocumentAppService>();

        return services;
    }
}
