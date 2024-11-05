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
    }

    private void ConfigureDatabase(IServiceCollection serviceCollection)
    {
        serviceCollection.AddDbContext<FlowerAppContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
        serviceCollection.AddScoped<IFlowersStorage, FlowersStorage>();
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