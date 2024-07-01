using LibraryManager.Api;
using LibraryManager.Api.Commons;
using LibraryManager.Api.Data;
using LibraryManager.Api.Repositories;
using LibraryManager.Api.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var stage = Configurations.DevelopmentStage;
builder.Services.AddEntityFrameworkSqlServer().AddDbContext<LibraryDbContext>(o =>
{
    o.UseSqlServer(stage == Configurations.DevelopmentStage
        ? builder.Configuration["SqlServer:ConnectionStrings"]
        : builder.Configuration["SqlServer:ConnectionStringsProd"]);
});

builder.Services.AddDbContext<LibraryMongoDbContext>(o =>
{
    o.UseMongoDB(stage == Configurations.DevelopmentStage
        ? builder.Configuration["MongoDb:ConnectionStrings"]
        : builder.Configuration["MongoDb:ConnectionStringsProd"], builder.Configuration["MongoDb:DataBase"]);
});

builder.Services.AddStackExchangeRedisCache(o =>
{
    o.Configuration = stage == Configurations.DevelopmentStage
    ? builder.Configuration["Redis:ConnectionStrings"]
    : builder.Configuration["Redis:ConnectionStringsProd"];

});

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();

builder.Services.AddSingleton<CacheHandler>();
builder.Services.AddSingleton<HashPassword>();

builder.Services.AddCors(o =>
{
    o.AddPolicy(Configurations.PolicyName,
        policy => policy
        .WithOrigins([Configurations.FrontEndURL, Configurations.BackEndURL])
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowCredentials());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(Configurations.PolicyName);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
