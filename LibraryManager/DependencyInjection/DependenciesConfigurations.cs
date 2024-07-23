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
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Builder;
using LibraryManager.Core.Services.Token;

namespace DependencyInjection
{
    public static class DependenciesConfigurations
    {
        public static void AddConfigurations(this IServiceCollection service, IConfiguration configuration, string applicationStage)
        {
            service.AddServicesDependencies(configuration, applicationStage);
            service.AddRepositoriesDependencies();
            service.AddUtilsDependencies();
            service.AddAuthConfigurations(configuration);
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
            service.AddSingleton<TokenGenerator>();

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

        public static void AddAuthConfigurations(this IServiceCollection service, IConfiguration configuration)
        {
            var key = Encoding.ASCII.GetBytes(configuration["JWT:Secret"]!);

            service.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = true;
                options.SaveToken = true;

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidIssuer = configuration["JWT:Issuer"],
                    ValidateAudience = true,
                    ValidAudience = configuration["JWT:Audience"]
                };

            });

            service.AddAuthorization(options =>
            {
                options.AddPolicy("UserPolicy", policy => policy.RequireRole("User"));
                options.AddPolicy("MerchantPolicy", policy => policy.RequireRole("Merchant"));
                options.AddPolicy("AdminPolicy", policy => policy.RequireRole("Admin"));
            });
            
        }

        public static void UseAuthConfigurations(this IApplicationBuilder app)
        {
            app.UseAuthentication();
            app.UseAuthorization();
        }

    }
}
