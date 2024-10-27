using FlowerApp.Service.Services;

namespace FlowerApp.Service.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection ConfigureServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IFlowersService, FlowersService>();
        return serviceCollection;
    }
}