using LibraryManager.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using LibraryManager.Domain.Interfaces;
using LibraryManager.Data.Helper.AWS;
using LibraryManager.Data.Helper;
using LibraryManager.Data.Context;
using LibraryManager.Data;
using Amazon.S3;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Builder;
using System.Security.Claims;
using LibraryManager.Application.Helper.Tokens.Interfaces;
using LibraryManager.Application.Helper.Tokens;
using LibraryManager.Application.Services.Interfaces;
using LibraryManager.Application.Services;

namespace LibraryManager.DI;

public static class DependenciesConfigurations
{
    public static void AddConfigurations(this IServiceCollection service, IConfiguration configuration, string applicationStage)
    {
        service.AddServicesDependencies(configuration, applicationStage);
        service.AddRepositoriesDependencies();
        service.AddServices();
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
        
    }

    public static void AddRepositoriesDependencies(this IServiceCollection service)
    {
        service.AddScoped<IUserRepository, UserRepository>();
        service.AddScoped<IBookRepository, BookRepository>();
        service.AddScoped<IAuthorRepository, AuthorRepository>();
        service.AddScoped<IOrderRepository, OrderRepository>();
    }

    public static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IBookService, BookService>();
        services.AddScoped<IAuthorService, AuthorService>();
        services.AddScoped<IOrderService, OrderService>();
    }

    public static void AddUtilsDependencies(this IServiceCollection service)
    {
        service.AddSingleton<CacheHandler>();
        service.AddSingleton<ITokensGenerator, TokensGenerator>();
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

            options.AddPolicy("EveryoneHasAccessPolicy", policy => policy.RequireAssertion(context => context.User.HasClaim(c =>
            (c.Type == ClaimTypes.Role && c.Value == "User") ||
            (c.Type == ClaimTypes.Role && c.Value == "Merchant") ||
            (c.Type == ClaimTypes.Role && c.Value == "Admin")
            )));

            options.AddPolicy("JustMerchantAndAdminPolicy", policy => policy.RequireAssertion(context => context.User.HasClaim(c =>
            (c.Type == ClaimTypes.Role && c.Value == "Merchant") ||
            (c.Type == ClaimTypes.Role && c.Value == "Admin")
            )));


        });
        
    }

    public static void UseAuthConfigurations(this IApplicationBuilder app)
    {
        app.UseAuthentication();
        app.UseAuthorization();
    }

}
