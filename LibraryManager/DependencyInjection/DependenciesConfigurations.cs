using Data.Context;
using Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using LibraryManager.Core.Interfaces;
using Data.Services.Utils;
using Data;
using Amazon.S3;
using Data.Services.APIs;

namespace DependencyInjection
{
    public static class DependenciesConfigurations
    {
        public static void AddConfigurations(this IServiceCollection service, IConfiguration configuration, string applicationStage)
        {
            service.AddServicesDependencies(configuration, applicationStage);
            service.AddRepositoriesDependencies();
            service.AddUtilsDependencies();
        }

        public static void AddServicesDependencies(this IServiceCollection service, IConfiguration configuration, string applicationStage)
        {

            service.AddEntityFrameworkSqlServer().AddDbContext<LibraryDbContext>(options =>
            {
                options.UseSqlServer(applicationStage == DataConfigurations.DevelopmentStage
                    ? configuration["SqlServer:ConnectionStrings"]
                    : configuration["SqlServer:ConnectionStringsProd"]);
            });

            service.AddEntityFrameworkMongoDB().AddDbContext<LibraryMongoDbContext>(options =>
            {
                options.UseMongoDB(applicationStage == DataConfigurations.DevelopmentStage
                    ? configuration["MongoDb:ConnectionStrings"]
                    : configuration["MongoDb:ConnectionStringsProd"], configuration["MongoDb:DataBase"]);
            });

            service.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = applicationStage == DataConfigurations.DevelopmentStage
                    ? configuration["Redis:ConnectionStrings"]
                    : configuration["Redis:ConnectionStringsProd"];
            });

            service.AddDefaultAWSOptions(configuration.GetAWSOptions());
            service.AddAWSService<IAmazonS3>().AddTransient<AWSS3>();


        }

        public static void AddRepositoriesDependencies(this IServiceCollection service)
        {
            service.AddScoped<IUserRepository, UserRepository>();
            service.AddScoped<IBookRepository, BookRepository>();
            service.AddScoped<IAuthorRepository, AuthorRepository>();
            service.AddScoped<IOrderRepository, OrderRepository>();
        }

        public static void AddUtilsDependencies(this IServiceCollection service)
        {
            service.AddSingleton<CacheHandler>();
            service.AddSingleton<HashPassword>();
        }

    }
}
