using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddOcelot();

builder.Services.AddCors(options=>
                        {options.AddPolicy("CorsPolicy", builder=>
                                {
                                    //адрес фронта
                                    builder.WithOrigins("http://test.ru")
                                        .AllowAnyMethod()
                                        .AllowAnyHeader(); 
                                });
                        });

var app = builder.Build();

// Configure the HTTP request pipeline.Ы

//app.UseHttpsRedirection();

app.UseCors("CorsPolicy");
await app.UseOcelot();

app.Run();
