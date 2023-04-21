using CSharp.Domain;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

public static class ConfigureServices
{
    public static IServiceCollection AddDomainLayer(
        this IServiceCollection services,
        IConfiguration config)
    {
        Action<DbContextOptionsBuilder> setupContext = opts =>
        {
            var connectionString = config["ConnectionStrings:Application"];
            opts.UseSqlite(connectionString);
        };

        services.AddDbContextFactory<ApplicationDataContext>(setupContext);
        services.AddDbContext<ApplicationDataContext>(setupContext);

        return services;
    }

    public static void UseDomainLayer(this IHost host)
    {
        using var scope = host.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDataContext>();
        // dotnet ef migrations add Initial -o=Migrations --startup-project=../CSharp.WebApi
        context.Database.Migrate();
    }
}