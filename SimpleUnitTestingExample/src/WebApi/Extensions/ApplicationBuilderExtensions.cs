using Microsoft.EntityFrameworkCore;
using Persistence;
using WebApi.Abstractions;

namespace WebApi.Extensions;

public static class ApplicationBuilderExtensions
{
    internal static IApplicationBuilder MapEndpoints(this WebApplication app)
    {
        var endpoints = app.Services.GetRequiredService<IEnumerable<IEndpoint>>();

        foreach (var endpoint in endpoints)
        {
            endpoint.MapEndpoint(app);
        }

        return app;
    }

    internal static void ApplyMigrations(this IApplicationBuilder builder)
    {
        using var scope = builder.ApplicationServices.CreateScope();

        using var applicationDbContext =
            scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        applicationDbContext.Database.EnsureCreated();

        applicationDbContext.Database.Migrate();
    }
}