using ET.Trans.Bus.DataBase;
using ET.Trans.Bus.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ET.JwtAuthManager;
using ET.Trans.Bus.RepoService;
using ET.Trans.Bus.Interface;
using ET.Trans.Bus.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddOption<AppOption>();
var appOption = builder.Services.BuildServiceProvider().GetRequiredService<IOptions<AppOption>>().Value;
builder.Services.AddDbContext<DatabaseContext>(provider => {
    provider.UseSqlServer(appOption.DBConnectionString);
});

builder.Services.AddScoped<ITransporterRepo, TransporterRepo>();
builder.Services.AddScoped<ITransporterService, TransporterService>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCustomJwtAuthentication();
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
