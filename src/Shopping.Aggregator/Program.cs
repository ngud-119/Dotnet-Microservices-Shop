using Shopping.Aggregator.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient<ICatalogService, CatalogService>(e =>
    e.BaseAddress = new Uri(builder.Configuration["ApiSettings:CatalogUrl"]!)
);

builder.Services.AddHttpClient<IBasketService, BasketService>(e =>
    e.BaseAddress = new Uri(builder.Configuration["ApiSettings:BasketUrl"]!)
);

builder.Services.AddHttpClient<IOrderService, OrderService>(e =>
    e.BaseAddress = new Uri(builder.Configuration["ApiSettings:OrderingUrl"]!)
);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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

app.UseAuthorization();

app.MapControllers();

app.Run();
