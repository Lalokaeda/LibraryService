using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookRentService.Application.Interfaces;

namespace BookRentService.Infrastructure.BackgroundJobs
{
    public class FineBackgroundService : BackgroundService
    {
        private IServiceScopeFactory _scopeFactory;

        public FineBackgroundService(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _scopeFactory.CreateScope())
                {
                    var fineService = scope.ServiceProvider.GetRequiredService<IFineService>();
                    await fineService.CheckAndApplyFinesAsync();
                }

                await Task.Delay(TimeSpan.FromDays(1), stoppingToken);
            }
        }
    }
}