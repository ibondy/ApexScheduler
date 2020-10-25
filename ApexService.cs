namespace ApexScheduler
{
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Logging;

    using System;
    using System.Linq;
    using System.Net.Http;
    using System.Text.Json;
    using System.Threading.Tasks;

    /// <summary>
    /// Communicates with Apex via RESTApi
    /// </summary>
    public class ApexService
    {
        private readonly IConfiguration _configuration;
        private readonly CookieDelegateHandler _cookieDelegateHandler;
        private readonly HttpClient _httpClient;
        private readonly ILogger<ApexService> _logger;
        private Config _apexConfig;

        public ApexService(HttpClient httpClient, IConfiguration configuration, CookieDelegateHandler cookieDelegateHandler, ILogger<ApexService> logger)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _cookieDelegateHandler = cookieDelegateHandler;
            _logger = logger;
        }

        public async Task GetLogin()
        {
            if (_apexConfig == null)
            {
                throw new ArgumentNullException("Context is not set");
            }

            var baseUri = string.Format($" {_apexConfig.Url}:{_apexConfig.Port}");
            var uri = string.Format($"{baseUri}/rest/login");
            var login = new ApexLoginContext { Password = _apexConfig.Password, User = _apexConfig.User };
            var payload = JsonSerializer.Serialize(login);

            _logger.Log(LogLevel.Information, null, "Login to Apex");
            var result = await _httpClient.PostAsync(uri, new StringContent(payload)).ConfigureAwait(false);
            if (result.IsSuccessStatusCode)
            {
                _logger.Log(LogLevel.Information, null, "Login into Apex successful");
                if (result.Headers.Contains("Set-Cookie"))
                {
                    _cookieDelegateHandler.AccessCookie = result.Headers.First(p => p.Key == "Set-Cookie").Value.First();
                }
            }
        }

        /// <summary>
        /// Sets Apex connection context
        /// </summary>
        /// <param name="apexConfig"> </param>
        public void SetContext(Config apexConfig)
        {
            _apexConfig = apexConfig;
            GetLogin().Wait();
        }

        public async Task RequestALKTest()
        {
            if (_apexConfig == null)
            {
                throw new ArgumentNullException("Context is not set");
            }

            var baseUri = string.Format($" {_apexConfig.Url}:{_apexConfig.Port}");
            var uri = string.Format($"{baseUri}/rest/status/outputs/{_apexConfig.AlkTestApexCommand.did}");
            var cmd = JsonSerializer.Serialize(_apexConfig.AlkTestApexCommand);
            var payload = new StringContent(cmd);

            var result = await _httpClient.PutAsync(uri, payload).ConfigureAwait(false);
            if (result.IsSuccessStatusCode)
            {
                _logger.LogInformation($"ALK test scheduled with Apex successfully {DateTime.Now.ToShortTimeString()}");
            }
        }

        public async Task RequestAllTests()
        {
            if (_apexConfig == null)
            {
                throw new ArgumentNullException("Context is not set");
            }

            var baseUri = string.Format($" {_apexConfig.Url}:{_apexConfig.Port}");
            var uri = string.Format($"{baseUri}/rest/status/outputs/{_apexConfig.AllTestsApexCommand.gid}");
            var cmd = JsonSerializer.Serialize(_apexConfig.AllTestsApexCommand);
            var payload = new StringContent(cmd);

            var result = await _httpClient.PutAsync(uri, payload).ConfigureAwait(false);
            if (result.IsSuccessStatusCode)
            {
                _logger.LogInformation($"ALK-CA-MG test scheduled with Apex successfully {DateTime.Now.ToShortTimeString()}");
            }
        }
    }
}