using Core;
using Domain.Services;
using Infrastructure.Data.Mongo;
using Infrastructure.Data.Mongo.Models;
using Infrastructure.Data.Mongo.Profiles;
using MessagePack.AspNetCoreMvcFormatter;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<MongoDbSettings>(
    builder.Configuration.GetSection("MongoDB"));

builder.Services.AddSingleton(sp =>
{
    var settings = sp.GetRequiredService<IOptions<MongoDbSettings>>().Value;
    return new MongoDbContext(settings.ConnectionString, settings.DatabaseName);
});

builder.Services.AddScoped<ICacheService, MemoryCacheService>();

builder.Services.AddScoped<IDocumentService, DocumentService>();

builder.Services.AddSingleton<IMemoryCache, MemoryCache>();

builder.Services.AddScoped<IDocumentRepository, MongoDocumentRepository>();

builder.Services.AddControllers(options =>
{
    options.InputFormatters.Add(new XmlSerializerInputFormatter(options));
    options.OutputFormatters.Add(new XmlSerializerOutputFormatter());

    options.InputFormatters.Add(new MessagePackInputFormatter());
    options.OutputFormatters.Add(new MessagePackOutputFormatter());
});

builder.Services.AddAutoMapper(typeof(DocumentProfile), typeof(Web.Profiles.DocumentProfile));

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

app.Run();