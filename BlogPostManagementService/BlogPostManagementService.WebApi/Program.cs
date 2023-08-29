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
using EmpCore.WebApi.Swagger;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var applicationAssembly = typeof(CreateDraftBlogPostCommand).Assembly;
var persistenceAssembly = typeof(IBlogPostDomainRepository).Assembly;

var oltpSqlConnectionString = builder.Configuration.GetConnectionString("OltpSqlConnectionString");
var readOnlySqlConnectionString = builder.Configuration.GetConnectionString("ReadOnlySqlConnectionString");
var azureServiceBusConnectionString = builder.Configuration.GetConnectionString("AzureServiceBusConnectionString");

// Add Crosscutting
var redisServer = builder.Configuration["Redis:Server"];
var redisInstanceName = builder.Configuration["Redis:InstanceName"];
builder.Services.AddRedisCache(redisServer, redisInstanceName);

// Add Infrastructure
builder.Services.AddApplicationDbContext(oltpSqlConnectionString, persistenceAssembly);
builder.Services.AddCapMessageBus(oltpSqlConnectionString, azureServiceBusConnectionString, applicationAssembly);

// Add Application
builder.Services.AddApplication(applicationAssembly);

// Add QueryStack
builder.Services.AddConnectionFactory(readOnlySqlConnectionString);

// Add Presentation
var identityServerUrl = new Uri(builder.Configuration["Auth:IdentityServerUrl"], UriKind.Absolute);
var audience = builder.Configuration["Auth:Audience"];

var swaggerOptions = new SwaggerOptions();
builder.Configuration.GetSection("Swagger").Bind(swaggerOptions);

builder.Services.AddControllers();
builder.Services.AddWebApiVersioning();
builder.Services.SlugifyWebApiUrls();
builder.Services.AddRateLimiting();
builder.Services.AddPrincipalUser();
builder.Services.AddApiAuth(identityServerUrl, audience);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerDocs(swaggerOptions);

var app = builder.Build();

app.UseSwaggerDocs();
app.UseRateLimiter();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
