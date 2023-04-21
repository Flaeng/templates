using CSharp.WebApi.Mapping;

public static class ConfigureServices
{
    public static IServiceCollection AddApplicationLayer(
        this IServiceCollection services,
        IConfiguration configuration
        )
    {
        services.AddAutoMapper(
            typeof(TodoItemProfile).Assembly
        );

        return services;
    }
    public static void UseApplicationLayer(this IHost host)
    { }
}