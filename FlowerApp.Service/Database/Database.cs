using FlowerApp.Data;
using FlowerApp.Service.Storages;
using Microsoft.EntityFrameworkCore;

namespace FlowerApp.Service.Database;

public static class Database
{
    public static IServiceCollection AddDatabase(this IServiceCollection serviceCollection,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        var environmentConnectionString = Environment.GetEnvironmentVariable("DefaultConnection");
        if (!string.IsNullOrEmpty(environmentConnectionString)) connectionString = environmentConnectionString;

        serviceCollection
            .AddDbContext<FlowerAppContext>(options => options.UseNpgsql(connectionString))
            .AddScoped<IFlowersStorage, FlowersStorage>()
            .AddScoped<IUserStorage, UserStorage>()
            .AddScoped<ITradeStorage, TradeStorage>();

        return serviceCollection;
    }
}