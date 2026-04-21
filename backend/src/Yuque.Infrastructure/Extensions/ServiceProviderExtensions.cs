using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Yuque.Infrastructure.Persistence;
using Yuque.Infrastructure.Persistence.Seed;

namespace Yuque.Infrastructure.Extensions;

public static class ServiceProviderExtensions
{
    public static async Task InitializeDatabaseAsync(this IServiceProvider serviceProvider, bool seedDevelopmentData)
    {
        using var scope = serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        await dbContext.Database.MigrateAsync();

        if (seedDevelopmentData)
        {
            var seeder = scope.ServiceProvider.GetRequiredService<DevelopmentDataSeeder>();
            await seeder.SeedAsync();
        }
    }
}