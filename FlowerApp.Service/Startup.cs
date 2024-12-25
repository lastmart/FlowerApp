using System.Text.Json.Serialization;
using FlowerApp.Service.Database;
using FlowerApp.Service.Extensions;
using FlowerApp.Service.Middlewares;

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
        serviceCollection
            .AddDatabase(configuration)
            .AddServices()
            .AddSwagger()
            .AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment environment, DataSeeder dataSeeder)
    {
        app.UseMiddleware<ExceptionHandlingMiddleware>();

        if (environment.IsDevelopment())
        {
            app
                .UseCors()
                .UseRouting()
                .UseEndpoints(endpoints => endpoints.MapControllers());
        }

        app
            .UseSwagger()
            .UseSwaggerUI();

        dataSeeder.SeedDataAsync().Wait();
    }
}