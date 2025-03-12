

using AramGeddon.webapi.Endpoints;
using MeleeAram.webapi.Data;
using MeleeAram.webapi.Entities;
using MeleeAram.webapi.Repository;


var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
string _connectionString = configuration.GetValue<string>("ConnectionStrings:DefaultConnectionString")!;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AramGeddonContext>();
builder.Services.AddScoped<IAgRepository<Champion>, AgRepository<Champion>>();


builder.Services.AddAutoMapper(typeof(Program));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
// Configure endpoints
app.ConfigureChampionEndpoints();



app.Run();
