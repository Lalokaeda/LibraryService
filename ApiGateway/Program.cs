using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddOcelot();

var app = builder.Build();

// Configure the HTTP request pipeline.Ы

app.UseHttpsRedirection();


await app.UseOcelot();

app.Run();
