using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace ApexScheduler
{
    public partial class ALKSchedule : IScheduledTask
    {
        private readonly ApexConfigs _apexConfigs;
        private readonly ApexService _apexService;
        private readonly IConfiguration _configuration;
        private readonly CookieDelegateHandler _cookieDelegateHandler;
        private readonly ILogger<ALKSchedule> _logger;

        public ALKSchedule(ApexService apexService, CookieDelegateHandler cookieDelegateHandler, ApexConfigs configs, ILogger<ALKSchedule> logger, IConfiguration configuration)
        {
            _configuration = configuration;
            _apexService = apexService;
            _cookieDelegateHandler = cookieDelegateHandler;
            _apexConfigs = configs;
            _logger = logger;
        }

        public async Task Invoke()
        {
            if (!string.IsNullOrEmpty(_apexConfigs.Apex1.ALK))
            {
                var apex1Times = _apexConfigs.Apex1.ALK.Split(',');
                if (apex1Times.Contains(DateTime.Now.Second.ToString()))
                {
                    Console.WriteLine($"Apex1 yes {DateTime.Now}");
                    _apexService.SetContext(_apexConfigs.Apex1);
                    await _apexService.RequestALKTest();
                }
            }

            if (!string.IsNullOrEmpty(_apexConfigs.Apex2.ALK))
            {
                var apex2Times = _apexConfigs.Apex2.ALK.Split(',');
                if (apex2Times.Contains(DateTime.Now.Second.ToString()))
                {
                    Console.WriteLine($"Apex2 yes {DateTime.Now}");
                }
            }

            Console.WriteLine(DateTime.Now);
            await Task.CompletedTask;
        }
    }
}