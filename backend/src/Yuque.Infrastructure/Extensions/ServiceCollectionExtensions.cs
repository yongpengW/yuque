using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Yuque.Application.Documents.Services;
using Yuque.Application.Repositories.Services;
using Yuque.Infrastructure.Persistence;
using Yuque.Infrastructure.Persistence.QueryServices;
using Yuque.Infrastructure.Persistence.Seed;
using Yuque.Infrastructure.Persistence.Services;

namespace Yuque.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection")
            ?? "Host=localhost;Port=5432;Database=yuque;Username=postgres;Password=postgres";

        services.AddDbContext<AppDbContext>(options => options.UseNpgsql(connectionString));
        services.AddScoped<DevelopmentDataSeeder>();
        services.AddScoped<IRepositoryQueryService, RepositoryQueryService>();
        services.AddScoped<IRepositoryCommandService, RepositoryCommandService>();
        services.AddScoped<IDocumentQueryService, DocumentQueryService>();
        services.AddScoped<IDocumentCommandService, DocumentCommandService>();

        return services;
    }
}
