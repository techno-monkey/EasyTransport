using ET.Orders.DataBase;
using ET.Orders.Interface;
using ET.Orders.Model;
using ET.Orders.RepoService;
using ET.Orders.Service;
using ET.Orders.ServiceBus;
using ET.ServiceBus;
using ET.ServiceBus.Events;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddOption<AppOption>();
var appOption = builder.Services.BuildServiceProvider().GetRequiredService<IOptions<AppOption>>().Value;
builder.Services.AddDbContext<DatabaseContext>(provider =>
{
    provider.UseSqlServer(appOption.DBConnectionString);
});

builder.Services.AddScoped<IOrderRepo, OrderRepo>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddSingleton<IServiceBusConnection>(provider =>
{
    var option = provider.GetRequiredService<IOptions<AppOption>>().Value;
    return new ServiceBusConnection(option.ServiceBusConnectionString);
});

builder.Services.AddSingleton<IEventBusSubscriptionsManager, EventBusSubscriptionsManager>();

builder.Services.AddSingleton<IEventBusServiceBus, EventBusServiceBus>(sp =>
{
    var serviceBusConnection = sp.GetRequiredService<IServiceBusConnection>();
    var logger = sp.GetRequiredService<ILogger<EventBusServiceBus>>();
    var eventBusSubscriptionsManager = sp.GetRequiredService<IEventBusSubscriptionsManager>();

    return new EventBusServiceBus(serviceBusConnection, logger,
        eventBusSubscriptionsManager, string.Empty, sp);
});



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

app.UseAuthorization();
app.UseAuthentication();

app.MapControllers();

app.Run();
