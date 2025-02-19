using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using BookRentService.Infrastructure.Messaging.Consumers;
using EventBus.Abstractions;
using EventBus.RabbitMq;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;

namespace BookRentService.Infrastructure
{
    public static class DependencyInjection
    {
        public static async Task<IServiceCollection> AddInfrastructureAsync(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DbConnectionString") ?? throw new InvalidOperationException("Connection string 'DbConnectionString' not found.");

            services.AddDbContext<BookRentDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
                options.EnableSensitiveDataLogging();
            }).AddTransient<BookRentDbContext>();

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
            services.AddFluentValidationAutoValidation()
                .AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
                
            var eventBus = new EventBus.RabbitMq.EventBus("localhost", "LibraryExchange");
                await eventBus.InitializeAsync();
                services.AddSingleton<IEventBus>(eventBus);



            services.AddSwaggerGen(c =>
            {
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            services.AddTransient<BookDeletedEventConsumer>();
            return services;
        }
    }
}