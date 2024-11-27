using System.Reflection;
using FlowerApp.Data.Storages;
using FlowerApp.Domain.Common;
using FlowerApp.Service.Common.Mappers;
using FlowerApp.Service.Database;
using FlowerApp.Service.Services;
using FlowerApp.Service.Validators;
using FluentValidation;
using Microsoft.OpenApi.Models;

namespace FlowerApp.Service.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddServices(this IServiceCollection serviceCollection)
    {
        serviceCollection
            .AddScoped<IFlowersService, FlowersService>()
            .AddScoped<IUserService, UserService>()
            .AddScoped<ITradeService, TradeService>()
            .AddScoped<DataSeeder>()
            .AddValidators()
            .AddAutoMappers();

        return serviceCollection;
    }

    public static IServiceCollection AddSwagger(this IServiceCollection serviceCollection)
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

            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            options.IncludeXmlComments(xmlPath);
        });

        return serviceCollection;
    }

    private static IServiceCollection AddAutoMappers(this IServiceCollection serviceCollection)
    {
        serviceCollection
            .AddAutoMapper(typeof(PageResponseProfile), typeof(FlowerProfile))
            .AddAutoMapper(typeof(UserProfile))
            .AddAutoMapper(typeof(TradeProfile));

        return serviceCollection;
    }

    private static IServiceCollection AddValidators(this IServiceCollection serviceCollection)
    {
        serviceCollection
            .AddScoped<IValidator<Pagination>, PaginationValidator>();

        return serviceCollection;
    }
}