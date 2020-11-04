using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Microsoft.RequestEcho
{
    sealed class ARMCertCache : IARMCertCache, IDisposable
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger _logger;
        private Timer _timer;
        private readonly ARMAuthOptions _armAuthOptions;
        ClientCertificatesResponse _clientCertificates;
        ManualResetEventSlim _certFetched = new ManualResetEventSlim(false);

        public ARMCertCache(
            HttpClient httpClient,
            IOptions<ARMAuthOptions> armAuthOptions,
            ILogger<ARMCertCache> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _armAuthOptions = armAuthOptions.Value ?? throw new ArgumentNullException(nameof(armAuthOptions));

            // Set default value according to the spec, 1 hour is recommended: https://armwiki.azurewebsites.net/authorization/AuthenticateBetweenARMandRP.html
            _timer = new Timer(TimerCallback, state: null, dueTime: 0, period: (int)TimeSpan.FromHours(1).TotalMilliseconds);
        }

        private async void TimerCallback(object state)
        {
            try
            {
                _clientCertificates = await _httpClient.GetAsync<ClientCertificatesResponse>(_armAuthOptions.ARMMetadataEndpoint);
            }
            catch (Exception ex)
            {
                // Log it
                _logger.LogError(ex, $"Can't fetch ARM certificate by endpoint: {_armAuthOptions.ARMMetadataEndpoint}");
                throw;
            }
            finally
            {
                _certFetched.Set();
            }
        }

        public void Dispose()
        {
            _timer?.Dispose();
            _timer = null;
        }

        public IEnumerable<string> ValidThumbprints
        {
            get
            {
                _certFetched.Wait();
                return _clientCertificates.ClientCertificates.Select(cert => cert.Thumbprint);
            }
        }
    }
}