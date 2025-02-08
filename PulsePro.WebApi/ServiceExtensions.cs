namespace PulsePro.WebApi;

public static class ServiceExtensions
{
    public static void ConfigureWebApi(this IServiceCollection services)
    { 
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
    }
}