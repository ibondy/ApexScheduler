using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace ApexScheduler
{
    public class SchedulerService : IHostedService
    {
        private readonly ILogger<SchedulerService> _logger;
        private readonly IServiceProvider _serviceProvider;
        private readonly Scheduler _scheduler;
        private readonly CancellationTokenSource _stoppingCts = new CancellationTokenSource();
        private System.Timers.Timer _timer;

        public SchedulerService(ILogger<SchedulerService> logger, IServiceProvider serviceProvider, Scheduler scheduler)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
            _scheduler = scheduler;
        }

        private async void OnElapsed(Object source, ElapsedEventArgs e)
        {
            await _scheduler.Execute();
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            if (_timer == null)
            {
                _timer = new System.Timers.Timer();
                _timer.Elapsed += new ElapsedEventHandler(OnElapsed);
                _timer.AutoReset = true;
                _timer.Interval = TimeSpan.FromMinutes(1).TotalMilliseconds;
            }

            _timer.Start();
            await Task.CompletedTask;
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            _timer.Stop();
            _timer.Dispose();
            await Task.CompletedTask;
        }
    }
}