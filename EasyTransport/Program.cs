using ET.JwtAuthManager;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<JwtTokenHandler>();
builder.Services.AddMvc();
var app = builder.Build();


app.UseAuthentication();
app.MapControllers();

app.Run();
