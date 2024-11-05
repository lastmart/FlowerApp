using FlowerApp.Domain.DTOModels;
using FlowerApp.Service.Controllers;
using FlowerApp.Service.Mappers;
using FlowerApp.Service.Services;
using FlowerApp.Service.Validators;
using FluentValidation;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

namespace FlowerApp.Service.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection ConfigureServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IFlowersService, FlowersService>();
        serviceCollection.AddScoped<DataSeeder>();
        serviceCollection.AddTransient<IValidator<FlowerFilterDto>, FlowerFilterDtoValidator>();
        serviceCollection.AddTransient<IValidator<FlowersPaginationRequest>, FlowersPaginationRequestValidator>();
        serviceCollection.ConfigureSwagger();
        serviceCollection.ConfigureAutoMapper();
        
        return serviceCollection;
    }

    private static IServiceCollection ConfigureSwagger(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "FlowerApp API",
                Version = "v1",
                Description = "API для управления цветами, включая фильтрацию и сортировку."
            });
            options.EnableAnnotations();
            options.ExampleFilters();
        });
        
        serviceCollection.AddSwaggerExamplesFromAssemblyOf<FlowerSortOptionsExample>();
        serviceCollection.AddSwaggerExamplesFromAssemblyOf<FlowerFilterExample>();
        
        return serviceCollection;
    }

    private static IServiceCollection ConfigureAutoMapper(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddAutoMapper(typeof(PageResponseProfile), typeof(FlowerProfile));
        return serviceCollection;
    }
}