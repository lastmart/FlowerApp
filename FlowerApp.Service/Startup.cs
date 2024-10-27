using FlowerApp.Data;
using FlowerApp.Data.Storages;
using FlowerApp.Domain.DbModels;
using FlowerApp.Service.Controllers;
using FlowerApp.Service.Extensions;
using FlowerApp.Service.Mappers;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

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
            options.ExampleFilters();
        });
        serviceCollection.AddSwaggerExamplesFromAssemblyOf<FlowerSortOptionsExample>();
        serviceCollection.AddSwaggerExamplesFromAssemblyOf<FlowerFilterExample>();
        serviceCollection.AddScoped<DataSeeder>();
        serviceCollection.AddAutoMapper(typeof(PagedResponseOffsetProfile), typeof(FlowerProfile));
    }

    private void ConfigureDatabase(IServiceCollection serviceCollection)
    {
        serviceCollection.AddDbContext<FlowerAppContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
        serviceCollection.AddScoped<IFlowerStorage, FlowerStorage>();
        serviceCollection.AddScoped<IStorageBase<LightParameters, int>, LightParametersStorage>();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment environment, DataSeeder dataSeeder)
    {
        if (environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseCors();
        app.UseRouting();
        app.UseEndpoints(endpoints => endpoints.MapControllers());
        dataSeeder.SeedDataAsync().Wait();
    }
}