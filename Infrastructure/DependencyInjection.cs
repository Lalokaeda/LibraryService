using System.Reflection;
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
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
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

            services.AddScoped<IBaseRepository<Book>, BookRepository>();
            services.AddScoped<IBaseRepository<BookExemplar>, BookExemplarRepository>();
            services.AddScoped<IBaseRepository<Author>, AuthorRepository>();

            return services;
        }
    }
}