using Microsoft.Extensions.DependencyInjection;
using PosIndonesia.Services;

namespace PosIndonesia;
public static class ConfigureServices
{
    public static IServiceCollection AddPosIndonesiaService(this IServiceCollection services)
    {
        services.AddSingleton<AppVersion>();
        services.AddSingleton<PostalInterop>();

        return services;
    }
}
