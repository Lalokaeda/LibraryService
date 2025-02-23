using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using BookRentService.Application.Handlers;
using BookRentService.Application.Interfaces;
using BookRentService.Domain.Entities;
using BookRentService.Domain.Interfaces;
using BookRentService.Infrastructure.BackgroundJobs;
using BookRentService.Infrastructure.Messaging.Consumers;
using BookRentService.Infrastructure.Repositories;
using BookRentService.Infrastructure.Service;
using EventBus.Abstractions;
using EventBus.RabbitMq;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BookRentService.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
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
                
            services.AddSingleton<IEventBus>(provider =>
            {
                var eventBus = new EventBus.RabbitMq.EventBus("localhost", "LibraryExchange");
                eventBus.InitializeAsync().GetAwaiter().GetResult(); 
                return eventBus;
            });

            services.AddHttpClient<BookService>();

            services.AddSingleton<BookDeletedEventConsumer>();
            services.AddScoped<IRequestHandler<Application.Commands.DeleteRentByBookIdCommand, bool>, BookDeletedHandler>();
            services.AddScoped<IBaseRepository<BookRent>, BookRentRepository>();
            services.AddScoped<IBaseRepository<BookExemplarRent>, BookExemplarRentRepository>();
            services.AddScoped<IBookService, BookService>();
            services.AddScoped<IFineRepository, FineRepository>();
            
            services.AddHostedService<FineBackgroundService>();

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