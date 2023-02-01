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

builder.Services.AddOcelot();

var app = builder.Build();


app.UseOcelot();
app.Run();
