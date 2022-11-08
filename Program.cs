using Microsoft.EntityFrameworkCore;
using WebTest.Context;
using WebTest.Contracts;
using WebTest.Services;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<FifaAppContext>(options => options.UseSqlServer(configuration.GetConnectionString("FifaAppContext")));

//Inject services
builder.Services.AddScoped<IPlayerService, PlayerService>();
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