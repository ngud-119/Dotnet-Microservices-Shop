using Microsoft.EntityFrameworkCore;
using Shopping.WebApp.Data;
using Shopping.WebApp.Repositories;
using Shopping.WebApp.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient<ICatalogService, CatalogService>(opt =>
{
    opt.BaseAddress = new Uri(builder.Configuration["ApiSettings:GatewayAddress"]!);
});
builder.Services.AddHttpClient<IBasketService, BasketService>(opt =>
{
    opt.BaseAddress = new Uri(builder.Configuration["ApiSettings:GatewayAddress"]!);
});
builder.Services.AddHttpClient<IOrderService, OrderService>(opt =>
{
    opt.BaseAddress = new Uri(builder.Configuration["ApiSettings:GatewayAddress"]!);
});

builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var loggerFactory = services.GetRequiredService<ILoggerFactory>();

    try
    {
        var context = services.GetRequiredService<ApplicationDbContext>();
        ApplicationDbContextSeed.SeedAsync(context, loggerFactory).Wait();
    }
    catch (Exception exception)
    {
        var logger = loggerFactory.CreateLogger<Program>();
        logger.LogError(exception, "An error occurred seeding the DB.");
    }
}

app.Run();
