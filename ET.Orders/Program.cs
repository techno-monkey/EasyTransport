using ET.Orders.DataBase;
using ET.Orders.Interface;
using ET.Orders.Model;
using ET.Orders.RepoService;
using ET.Orders.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddOption<AppOption>();
var appOption = builder.Services.BuildServiceProvider().GetRequiredService<IOptions<AppOption>>().Value;
builder.Services.AddDbContext<DatabaseContext>(provider => {
    provider.UseSqlServer(appOption.DBConnectionString);
});

builder.Services.AddScoped<IOrderRepo, OrderRepo>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


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
