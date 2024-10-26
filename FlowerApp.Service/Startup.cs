using FlowerApp.Data;
using FlowerApp.Data.Storages;
using FlowerApp.Domain.DbModels;
using FlowerApp.Service.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace FlowerApp.Service;

public class Startup
{
    private readonly IConfiguration configuration;

    public Startup(IConfiguration configuration)
    {
        this.configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection serviceCollection)
    {
        ConfigureDatabase(serviceCollection);
        serviceCollection.ConfigureServices();
        serviceCollection.AddControllers();
        serviceCollection.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "FlowerApp API",
                Version = "v1",
                Description = "API для управления цветами, включая фильтрацию и сортировку.",
            });
            options.EnableAnnotations();
        });
    }

    private void ConfigureDatabase(IServiceCollection serviceCollection)
    {
        serviceCollection.AddDbContext<FlowerAppContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
        serviceCollection.AddScoped<IStorageBase<Flower, int>, FlowerStorage>();
        serviceCollection.AddScoped<IStorageBase<LightParameters, int>, LightParametersStorage>();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment environment)
    {
        if (environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseCors();
        app.UseRouting();
        app.UseEndpoints(endpoints => endpoints.MapControllers());
    }
}