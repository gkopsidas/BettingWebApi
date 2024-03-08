using BettingApi.BusinessLogic;
using BettingApi.Models.EF;
using BettingApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var myConfig = new ConfigurationBuilder().AddJsonFile("appsettings.Development.json").Build();
var connectionString = myConfig.GetConnectionString("BettingsDB");

builder.Services.AddScoped<BettingsDbContext>();
builder.Services.AddScoped<IBettingLogic, BettingLogic>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.EnableAnnotations();
});
builder.Services.AddHealthChecks();

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

app.MapHealthChecks("/healthcheck");

app.Run();
