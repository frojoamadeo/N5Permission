using AppServices;
using Domain.Settings;
using Microsoft.EntityFrameworkCore;
using Persistance;
using Serilog;
using Serilog.Exceptions;
using Serilog.Sinks.Elasticsearch;
using Services;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssemblies(typeof(Program).Assembly);
});

builder.Services.AddMediatR([typeof(AppServices.StartupExtensions).Assembly]);

//Register EF
var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING") ?? builder.Configuration.GetConnectionString("Default");
builder.Services.AddEntityFramewrokService(connectionString!);

////Setup Elasticsearch
//builder.Services.Configure<ElasticSettings>(builder.Configuration.GetSection("ElasticSettings"));

//Register Services and Repositories
builder.Services.RegisterServicesAndRepositories();

configureLoggin();
builder.Host.UseSerilog();
//builder.Host.UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

if (app.Environment.IsDevelopment())
{
    using (var scope = app.Services.CreateScope())
    {
        //replace DataContext with your Db Context name
        var dataContext = scope.ServiceProvider.GetRequiredService<PermissionDbContext>();
        dataContext.Database.Migrate();
    }
}

app.Run();

void configureLoggin()
{
    var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
    var configuration = new ConfigurationBuilder()
        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        .AddJsonFile($"appsettings.{environment}.json", optional: true)
        .Build();

    Log.Logger = new LoggerConfiguration()
        .Enrich.FromLogContext()
        .Enrich.WithExceptionDetails()
        .WriteTo.Debug()
        .WriteTo.Console()
        .WriteTo.Elasticsearch(ConfigureElasticSink(configuration, environment))
        .Enrich.WithProperty("Environment", environment)
        .ReadFrom.Configuration(configuration)
        .CreateLogger();
}

ElasticsearchSinkOptions ConfigureElasticSink(IConfigurationRoot configurationRoot, string environment)
{
    //var uri = Environment.GetEnvironmentVariable("PRIVATE_IP") ?? configurationRoot["ElasticConfiguration:Uri"];
    var uri = Environment.GetEnvironmentVariable("ELASTICSEARCH_URI") ?? configurationRoot["ElasticConfiguration:Uri"];

    return new ElasticsearchSinkOptions(new Uri(uri))
    {
        AutoRegisterTemplate = true,
        IndexFormat = $"{Assembly.GetExecutingAssembly().GetName().Name.ToLower().Replace(".", "-") }-{environment.ToLower()}-{DateTime.UtcNow:yyyy-MM}",
        NumberOfReplicas = 1,
        NumberOfShards = 2
    };
}