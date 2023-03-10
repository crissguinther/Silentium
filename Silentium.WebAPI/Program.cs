using Microsoft.EntityFrameworkCore;
using Silentium.Data.Context;
using Silentium.WebAPI.Config;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
bool isDevelopment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development";

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<SilentiumContext>(options => 
    options.UseMySql(builder.Configuration.GetConnectionString("SilentiumDatabase"), 
    ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("SilentiumDatabase"))
));
builder.Services.AddScoped<ApplicationConfig>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();