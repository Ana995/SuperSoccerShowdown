using SuperSoccerShowdown.Application.Interfaces;
using SuperSoccerShowdown.Application.Services;
using SuperSoccerShowdown.Application.Simulator;
using SuperSoccerShowdown.Application.Strategies;
using SuperSoccerShowdown.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Register services
builder.Services.AddHttpClient();
builder.Services.AddScoped<ITeamFormationStrategy, DefaultTeamFormationStrategy>();
builder.Services.AddScoped<TeamGeneratorService>();

builder.Services.AddHttpClient<SwapiClient>();
builder.Services.AddHttpClient<PokeApiClient>();


// Register both universe clients for direct access
builder.Services.AddScoped<SwapiClient>();
builder.Services.AddScoped<PokeApiClient>();
builder.Services.AddScoped<IUniverseApiClientFactory, UniverseApiClientFactory>();
builder.Services.AddScoped<TeamGeneratorService>();
builder.Services.AddScoped<MatchSimulator>();
builder.Services.AddMemoryCache();


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.EnableAnnotations();
});


var app = builder.Build();

// Configure middleware
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();