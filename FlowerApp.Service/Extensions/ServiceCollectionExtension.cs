using System.Reflection;
using FlowerApp.DTOs.Common;
using FlowerApp.Service.Clients;
using FlowerApp.Service.Common.Mappers;
using FlowerApp.Service.Common.Validators;
using FlowerApp.Service.Configuration;
using FlowerApp.Service.Database;
using FlowerApp.Service.Handlers;
using FlowerApp.Service.Services;
using FlowerApp.Service.Storages;
using FlowerApp.Service.Storages.SurveyStorages;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;

namespace FlowerApp.Service.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddServices(this IServiceCollection serviceCollection)
    {
        serviceCollection
            .AddConfiguration<ApiSettings>()
            .AddConfiguration<ApiSecretSettings>()
            .AddScoped<IGoogleAuthService, GoogleAuthService>()
            .AddScoped<IAuthorizationContext, AuthorizationContext>()
            .AddScoped<IRecommendationService, RecommendationService>()
            .AddScoped<IRecommendationSystemClient, PythonRecommendationSystemClient>()
            .AddScoped<ITradeService, TradeService>()
            .AddStorages()
            .AddHandlers()
            .AddScoped<DataSeeder>()
            .AddValidators()
            .AddAutoMappers()
            .AddHttpClient();
        ;

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
            .AddAutoMapper(typeof(UserProfile))
            .AddAutoMapper(typeof(TradeProfile))
            .AddAutoMapper(typeof(SurveyProfile))
            .AddAutoMapper(typeof(QuestionProfile));
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
            .AddScoped<ITradeStorage, TradeStorage>()
            .AddScoped<ISurveyStorage, SurveyStorage>()
            .AddScoped<ISurveyAnswerStorage, SurveyAnswerStorage>()
            .AddScoped<ISurveyQuestionsStorage, SurveyQuestionsStorage>();
    }

    private static IServiceCollection AddHandlers(this IServiceCollection serviceCollection)
    {
        return serviceCollection
            .AddScoped<IAuthorizationHandler, GoogleAuthorizationHandler>();
    }

    private static IServiceCollection AddConfiguration<TConfig>(this IServiceCollection serviceCollection)
        where TConfig : class
    {
        return serviceCollection
            .AddSingleton<Func<TConfig>>(
                provider => () => provider.GetRequiredService<IConfiguration>().Get<TConfig>());
    }
}