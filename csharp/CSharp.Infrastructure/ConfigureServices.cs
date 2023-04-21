using CSharp.Infrastructure.Providers;
using CSharp.Infrastructure.Services;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration config,
        bool isDevelopment)
    {
        services.AddOptions<JwtOptions>().Bind(config.GetSection("Jwt"));
        services.AddOptions<SmtpOptions>().Bind(config.GetSection("SMTP"));

        services.AddTransient<IDateTimeProvider, DateTimeProvider>();
        services.AddTransient<IJwtTokenGenerator, JwtTokenProvider>();
        services.AddTransient<IJwtTokenValidator, JwtTokenProvider>();

        if (isDevelopment)
        {
            services.AddTransient<IMailProvider, FakeMailProvider>();
        }
        else
        {
            services.AddTransient<IMailProvider, MailProvider>();
        }

        services.AddTransient<IPasswordHasher, PasswordProvider>();
        services.AddTransient<IPasswordVerifier, PasswordProvider>();
        services.AddTransient<IStringGenerator, StringGenerator>();

        services.AddTransient<IUserService, UserService>();
        services.AddTransient<ITodoItemService, TodoItemService>();
        services.AddTransient<ITodoListService, TodoListService>();

        return services;
    }

    public static void UseInfrastructure(this IHost host)
    {
        using var scope = host.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDataContext>();
        context.Database.Migrate();
    }
}