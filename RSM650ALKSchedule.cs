using ApexScheduler.RavenDB;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApexScheduler
{
    public class RSM650ALKSchedule : IScheduledTask
    {
        private readonly ApexConfigs _apexConfigs;
        private readonly ApexService _apexService;
        private readonly ILogger<ALKSchedule> _logger;

        public RSM650ALKSchedule(ApexService apexService, ApexConfigs configs, ILogger<ALKSchedule> logger)
        {
            _apexService = apexService;
            _apexConfigs = configs;
            _logger = logger;
        }

        public async Task Invoke()
        {
            var executionList = new List<DateTime>();
            var times = _apexConfigs.Apex1.ALK.Split(",");
            if (times.Length == 1 & string.IsNullOrEmpty(times[0]))
            {
                _logger.LogInformation($"Apex1 Alk test schedule not configured");
                return;
            }
            foreach (var time in times)
            {
                var splitTime = time.Split(":");
                if (splitTime.Length == 2)
                {
                    executionList.Add(new DateTime(1, 1, 1, Convert.ToInt32(splitTime[0]), Convert.ToInt32(splitTime[1]), 0, 0));
                }
            }

            var currentTime = DateTime.Now;

            if (executionList.Any(p => p.Hour == currentTime.Hour && p.Minute == currentTime.Minute))
            {
                _logger.LogInformation($"Apex1 Alk test requested {DateTime.Now}");
                _apexService.SetContext(_apexConfigs.Apex1);
                await _apexService.RequestALKTest();
                return;
            }

            _logger.LogInformation($"RSM650AlkSchedule invoke completed {DateTime.Now.ToShortDateString()}:{DateTime.Now.ToShortTimeString()}");
            await Task.CompletedTask;
        }
    }

    public class RSM650ALLSchedule : IScheduledTask
    {
        private readonly ApexConfigs _apexConfigs;
        private readonly ApexService _apexService;
        private readonly ILogger<ALKSchedule> _logger;

        public RSM650ALLSchedule(ApexService apexService, ApexConfigs configs, ILogger<ALKSchedule> logger)
        {
            _apexService = apexService;
            _apexConfigs = configs;
            _logger = logger;
        }

        public async Task Invoke()
        {
            var executionList = new List<DateTime>();
            var times = _apexConfigs.Apex1.AlkCaMg.Split(",");
            if (times.Length == 1 & string.IsNullOrEmpty(times[0]))
            {
                _logger.LogInformation($"Apex1 Alk-Ca-Mg tests schedule not configured");
                return;
            }
            foreach (var time in times)
            {
                var splitTime = time.Split(":");
                if (splitTime.Length == 2)
                {
                    executionList.Add(new DateTime(1, 1, 1, Convert.ToInt32(splitTime[0]), Convert.ToInt32(splitTime[1]), 0, 0));
                }
            }

            var currentTime = DateTime.Now;

            if (executionList.Any(p => p.Hour == currentTime.Hour && p.Minute == currentTime.Minute))
            {
                _logger.LogInformation($"Apex1 Alk-Ca-Mg tests requested {DateTime.Now}");
                _apexService.SetContext(_apexConfigs.Apex1);
                await _apexService.RequestAllTests();
                return;
            }

            _logger.LogInformation($"RSM650AllSchedule invoke completed {DateTime.Now.ToShortDateString()}:{DateTime.Now.ToShortTimeString()}");

            await Task.CompletedTask;
        }
    }
}