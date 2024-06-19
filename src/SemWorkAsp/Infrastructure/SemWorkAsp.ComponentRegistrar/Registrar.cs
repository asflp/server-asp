using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using SemWorkAsp.ComponentRegistrar.Mappers;
using SemWorkAsp.Contracts.Dtos;

namespace SemWorkAsp.ComponentRegistrar;

public static class Registrar
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        return services.ConfigureAutomapper();
    }

    private static IServiceCollection ConfigureAutomapper(this IServiceCollection services)
    {
        return services.AddSingleton<IMapper>(new Mapper(GetMapperConfiguration()));
    }

    private static MapperConfiguration GetMapperConfiguration() 
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<UserProfile>();
            cfg.AddProfile<AdvertisementProfile>();
        });
        config.CreateMapper();
        return config;
    }
}