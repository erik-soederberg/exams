using GameLibraryData;
using Microsoft.EntityFrameworkCore;
using GameLibraryServices.Interfaces;
using GameLibraryServices.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IGenreService, GenreService>();
builder.Services.AddScoped<IGameService, GameService>();
builder.Services.AddScoped<IPlatformService, PlatformService>();

builder.Services.AddControllers();

builder.Services.AddOpenApi();

var app = builder.Build();

app.MapControllers();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();


app.Run();

