using FlowerApp.Data;
using Microsoft.EntityFrameworkCore;

namespace FlowerApp.Service.Database;

public static class Database
{
    public static IServiceCollection AddDatabase(this IServiceCollection serviceCollection,
        IConfiguration configuration)
    {
        var connectionString = Environment.GetEnvironmentVariable("DefaultConnection") ??
                               configuration.GetConnectionString("DefaultConnection");

        return serviceCollection.AddDbContext<FlowerAppContext>(options => options.UseNpgsql(connectionString));
    }
}