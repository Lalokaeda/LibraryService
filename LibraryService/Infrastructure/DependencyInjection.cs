using System.Reflection;
using EventBus.Abstractions;
using FluentValidation;
using FluentValidation.AspNetCore;
using LibraryService.Domain;
using LibraryService.Domain.Interfaces;
using LibraryService.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Serialization;

namespace LibraryService.Infrastructure
{
    public static class DependencyInjection
    {
        public static async Task<IServiceCollection> AddInfrastructureAsync(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DbConnectionString") ?? throw new InvalidOperationException("Connection string 'DbConnectionString' not found.");

            services.AddDbContext<LibraryDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
                options.EnableSensitiveDataLogging();
            }).AddTransient<LibraryDbContext>();

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
            services.AddFluentValidationAutoValidation()
                .AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            var eventBus = new EventBus.RabbitMq.EventBus("localhost", "LibraryExchange");
                await eventBus.InitializeAsync();
                services.AddSingleton<IEventBus>(eventBus);

            services.AddScoped<IBaseRepository<Book>, BookRepository>();
            services.AddScoped<IBaseRepository<BookExemplar>, BookExemplarRepository>();
            services.AddScoped<IBaseRepository<Author>, AuthorRepository>();

            services.AddSwaggerGen(c =>
            {
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            return services;
        }
    }
}