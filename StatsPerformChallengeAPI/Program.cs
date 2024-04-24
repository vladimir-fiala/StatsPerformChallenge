using StatsPerformChallengeAPI.MappingProfiles;
using StatsPerformChallengeAPI.Models;
using StatsPerformChallengeAPI.Models.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));
builder.Services.AddScoped<IProtectedDataAccess, ProtectedDataAccess>();
builder.Services.AddAutoMapper(typeof(BrandingDefinitionProfile));
builder.Services.AddAutoMapper(typeof(BrandingProfile));
builder.Services.AddAutoMapper(typeof(LeagueProfile));
builder.Services.AddAutoMapper(typeof(MatchProfile));
builder.Services.AddAutoMapper(typeof(TeamProfile));

var app = builder.Build();
Console.WriteLine("app.Environment.IsDevelopment():" + app.Environment.IsDevelopment());
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
