using Microsoft.EntityFrameworkCore;
using PlatformService.Models.Data;
using PlatformService.SyncDataServices;
using PlatformService.SyncDataServices.Http;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
//if(builder.Environment.IsProduction())
//{
//    Console.WriteLine("--> Using SqlServer Db");
//    builder.Services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("PlatformConn")));
//}
//else
//{
//    Console.WriteLine("--> Using InMem Db");
//    builder.Services.AddDbContext<AppDbContext>(opt => opt.UseInMemoryDatabase("InMem"));
//}
builder.Services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("PlatformConn")));

builder.Services.AddScoped<IPlatformRepo, PlatformRepo>();

builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddMaps(typeof(Program).Assembly);
});

builder.Services.AddHttpClient<ICommandDataClient, CommandDataClient>(client =>
{
    Console.WriteLine(client.BaseAddress);
});
var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}
PrepDb.PrepPopulation(app, true);

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();
