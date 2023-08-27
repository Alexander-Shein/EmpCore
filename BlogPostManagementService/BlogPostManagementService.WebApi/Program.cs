using BlogPostManagementService.Application.BlogPosts.Commands.CreateDraftBlogPost;
using BlogPostManagementService.Persistence.BlogPosts.DomainRepositories;
using EmpCore.Api.Middleware.RateLimiting;
using EmpCore.Api.Middleware.Security;
using EmpCore.Api.Middleware.SlugifyUrl;
using EmpCore.Api.Middleware.WebApiVersioning;
using EmpCore.Application;
using EmpCore.Crosscutting.DistributedCache;
using EmpCore.Infrastructure.MessageBus.CAP;
using EmpCore.Persistence.EntityFrameworkCore;
using EmpCore.QueryStack.Dapper;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var oltpSqlConnectionString = builder.Configuration.GetConnectionString("OltpSqlConnectionString");
var readOnlySqlConnectionString = builder.Configuration.GetConnectionString("ReadOnlySqlConnectionString");
var azureServiceBusConnectionString = builder.Configuration.GetConnectionString("AzureServiceBusConnectionString");

// Add Crosscutting
var redisServer = builder.Configuration["Redis.Server"];
var redisInstanceName = builder.Configuration["Redis.InstanceName"];
builder.Services.AddRedisCache(redisServer, redisInstanceName);

// Add Infrastructure
var persistenceAssembly = typeof(IBlogPostDomainRepository).Assembly;
builder.Services.AddApplicationDbContext(oltpSqlConnectionString, persistenceAssembly);
builder.Services.AddCapMessageBus(oltpSqlConnectionString, azureServiceBusConnectionString);

// Add Application
var applicationAssembly = typeof(CreateDraftBlogPostCommand).Assembly;
builder.Services.AddApplication(applicationAssembly);

// Add QueryStack
builder.Services.AddConnectionFactory(readOnlySqlConnectionString);

// Add Presentation
var identityServerUrl = new Uri(builder.Configuration["Auth.IdentityServerUrl"], UriKind.Absolute);
var audience = builder.Configuration["Auth.Audience"];

builder.Services.AddControllers();
builder.Services.AddWebApiVersioning();
builder.Services.SlugifyWebApiUrls();
builder.Services.AddRateLimiting();
builder.Services.AddPrincipalUser();
builder.Services.AddApiAuth(identityServerUrl, audience);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//app.UseAuthorization();

app.MapControllers();

app.Run();
