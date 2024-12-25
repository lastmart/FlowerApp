using System.Reflection;
using FlowerApp.DTOs.Common;
using FlowerApp.Service.Common.Mappers;
using FlowerApp.Service.Common.Validators;
using FlowerApp.Service.Database;
using FlowerApp.Service.Services;
using FlowerApp.Service.Storages;
using FluentValidation;
using Microsoft.OpenApi.Models;

namespace FlowerApp.Service.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddServices(this IServiceCollection serviceCollection)
    {
        serviceCollection
            // .AddScoped<IRecommendationService, RecommendationService>()
            // .AddScoped<IQuestionsStorage, QuestionsStorage>()
            // .AddScoped<IUserAnswersStorage, UserAnswersStorage>()
            // .AddScoped<IUserStorage, UsersStorage>()
            // .AddScoped<IRecommendationSystemClient, PythonRecommendationSystemClient>()
            // .AddScoped<IUserService, UserService>()
            // .AddScoped<ITradeService, TradeService>()
            .AddFlowerServices()
            .AddStorages()
            .AddScoped<DataSeeder>()
            .AddValidators()
            .AddAutoMappers()
            .AddHttpClient();

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
        return serviceCollection
            .AddAutoMapper(typeof(PageResponseProfile))
            .AddAutoMapper(typeof(FlowerSortOptionProfile));
        // .AddAutoMapper(typeof(UserProfile))
        // .AddAutoMapper(typeof(TradeProfile));
        // serviceCollection.AddAutoMapper(typeof(QuestionProfile));
    }

    private static IServiceCollection AddFlowerServices(this IServiceCollection serviceCollection)
    {
        return serviceCollection
            .AddScoped<IFlowersService, FlowersService>();
    }

    private static IServiceCollection AddValidators(this IServiceCollection serviceCollection)
    {
        serviceCollection
            .AddScoped<IValidator<Pagination>, PaginationValidator>();

        return serviceCollection;
    }

    private static IServiceCollection AddStorages(this IServiceCollection serviceCollection)
    {
        return serviceCollection
            .AddScoped<IFlowersStorage, FlowersStorage>()
            .AddScoped<IUserStorage, UsersStorage>()
            .AddScoped<ITradeStorage, TradeStorage>();
    }
}