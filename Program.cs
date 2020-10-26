using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.EventLog;
using System.Net.Http;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using ApexScheduler.RavenDB;

namespace ApexScheduler
{
    internal class Program
    {
        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            string environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            Console.WriteLine("Welcome to ApexScheduler");
            //var host = new HostBuilder()
            var host = Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostContext, builder) =>
                {
                    hostContext.HostingEnvironment.EnvironmentName = environment;
                    if (hostContext.HostingEnvironment.IsDevelopment())
                    {
                        builder.AddUserSecrets<Program>();
                    }
                    else
                    {
                        builder.AddJsonFile("settings.json", true);
                    }
                })
                .ConfigureServices((hostingContext, services) =>
                {
                    services.AddSingleton(Configure(hostingContext.Configuration));
                    services.AddSingleton<CookieDelegateHandler>();
                    services.AddHostedService<SchedulerService>();
                    services.AddHttpClient<ApexService>().AddHttpMessageHandler<CookieDelegateHandler>();
                    services.AddTransient<IScheduledTask, RSM650ALKSchedule>();
                    services.AddTransient<IScheduledTask, RSM650ALLSchedule>();
                    services.AddTransient<Scheduler>();
                    services.AddTransient<ALKSchedule>();
                    services.AddSingleton<RavenDBClient>();
                })
                .ConfigureLogging((hostingContext, logging) =>
                {
                    logging.AddConfiguration(hostingContext.Configuration);
                    if (hostingContext.HostingEnvironment.IsDevelopment())
                    {
                        logging.AddConsole();
                        logging.AddDebug();
                        logging.AddEventLog();
                    }

                    logging.AddEventLog(new EventLogSettings()
                    {
                        SourceName = "ApexScheduler"
                    });
                })
                .UseWindowsService();
            return host;
        }

        private static ApexConfigs Configure(IConfiguration config)
        {
            var apexConfigs = new ApexConfigs();
            apexConfigs.Apex1.Name = config["Apex1:Name"];
            apexConfigs.Apex1.Url = config["Apex1:Url"];
            apexConfigs.Apex1.Port = Convert.ToInt32(config["Apex1:Port"]);
            apexConfigs.Apex1.User = config["Apex1:User"];
            apexConfigs.Apex1.Password = config["Apex1:Password"];
            apexConfigs.Apex1.Active = Convert.ToBoolean(config["Apex1:Active"]);
            apexConfigs.Apex1.ALK = config["Apex1:ALK"];
            apexConfigs.Apex1.AlkCaMg = config["Apex1:AlkCaMg"];
            apexConfigs.Apex1.AlkTestApexCommand = new ApexCommand
            {
                status = new List<string> { "ON", "", "OK", "" },
                name = config["Apex1:AlkTest:Name"],
                gid = "",
                type = "selector",
                ID = Convert.ToInt32(config["Apex1:AlkTest:ID"]),
                did = config["Apex1:AlkTest:Did"]
            };

            apexConfigs.Apex1.AllTestsApexCommand = new ApexCommand
            {
                status = new List<string> { "ON", "", "OK", "" },
                name = config["Apex1:AllTests:Name"],
                gid = "",
                type = "selector",
                ID = Convert.ToInt32(config["Apex1:AllTests:ID"]),
                did = config["Apex1:AllTests:Did"]
            };

            apexConfigs.Apex2.Name = config["Apex2:Name"];
            apexConfigs.Apex2.Url = config["Apex2:Url"];
            apexConfigs.Apex2.Port = Convert.ToInt32(config["Apex2:Port"]);
            apexConfigs.Apex2.User = config["Apex2:User"];
            apexConfigs.Apex2.Password = config["Apex2:Password"];
            apexConfigs.Apex2.Active = Convert.ToBoolean(config["Apex2:Active"]);
            apexConfigs.Apex2.ALK = config["Apex2:ALK"];

            apexConfigs.RavenDBConfig.DatabaseName = config["RavenDB:DatabaseName"];
            apexConfigs.RavenDBConfig.Url = new[] { config["RavenDB:Url"] };

            return apexConfigs;
        }

        private static async System.Threading.Tasks.Task Main(string[] args)
        {
            await CreateHostBuilder(args).Build().RunAsync();
        }
    }
}