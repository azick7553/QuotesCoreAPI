using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using QuotesCoreAPI.Controllers;

namespace QuotesCoreAPI.Workers
{

    public class RemoveQuotesWorker : IHostedService
    {
        private Timer _timer;

        public Task StartAsync(CancellationToken stoppingToken)
        {
            _timer = new Timer(call, null, 0, 300000); //Every 5 minutes

            return Task.CompletedTask;
        }

        private void call(object state)
        {
            QuotesAPIController.QuotesList.RemoveAll(p => DateTime.Now.Subtract(p.CreatedDate).TotalMinutes > 60);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }
    }

}