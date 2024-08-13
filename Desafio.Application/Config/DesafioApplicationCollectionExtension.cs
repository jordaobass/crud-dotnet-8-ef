using System.Reflection;
using AutoMapper;
using Desafio.Application.Repository;
using Desafio.Application.Repository.Interfaces;
using Desafio.Application.Service;
using Desafio.Application.Service.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Desafio.Application.Config;

public static class DesafioApplicationCollectionExtension
{
    public static void AddDesafioApplication(this IServiceCollection serviceCollection,
        IConfiguration configuration)

    {
        var dataAccess = Assembly.GetExecutingAssembly();

        dataAccess.DefinedTypes
            .Where(t => t.IsSubclassOf(typeof(Profile)))
            .ToList()
            .ForEach(t => serviceCollection.AddAutoMapper(cfg => cfg.AddProfile(t)));
    }

    public static IServiceCollection AddDependencyInjectionConfiguration(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddScoped<ISaleService, SaleService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IProductService, ProductService>();


        services.AddTransient<IUserRepository, UserRepository>();
        services.AddTransient<IProductRespository, ProductRespository>();
        services.AddTransient<ISaleRespository, SaleRespository>();

        services.AddMemoryCache();
        return services;
    }
}