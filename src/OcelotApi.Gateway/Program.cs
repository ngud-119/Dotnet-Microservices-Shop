using Ocelot.Cache.CacheManager;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.AddConfiguration(builder.Configuration.GetSection("Logging"));
builder.Logging.AddConsole();
builder.Logging.AddDebug();

builder.Host.ConfigureAppConfiguration(opt =>
{
    opt.AddJsonFile($"ocelot.{builder.Environment.EnvironmentName}.json");
});

builder.Services.AddOcelot()
.AddCacheManager(opt =>
{
    opt.WithDictionaryHandle();
});

var app = builder.Build();


app.UseOcelot();
app.Run();
