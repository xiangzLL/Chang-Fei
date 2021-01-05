using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Orleans;

namespace ChangFei.Console
{
    public class ClusterClientHostedService:IHostedService
    {
        private readonly ILogger<ClusterClientHostedService> _logger;

        public IClusterClient Client { get; }

        public ClusterClientHostedService(ILogger<ClusterClientHostedService> logger)
        {
            _logger = logger;
            Client = new ClientBuilder()
                .UseLocalhostClustering()
                .Build();
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Connecting...");
            var retries = 100;
            await Client.Connect(async error =>
            {
                if (--retries < 0)
                {
                    _logger.LogError($"Could not connect to the cluster:{error.Message}");
                    return false;
                }

                _logger.LogWarning(error, "Error Connecting: {@Message}", error.Message);

                try
                {
                    await Task.Delay(1000, cancellationToken);
                }
                catch (OperationCanceledException)
                {
                    return false;
                }
                return true;
            });
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            var cancellation = new TaskCompletionSource<bool>();
            cancellationToken.Register(() => { cancellation.TrySetCanceled(cancellationToken); });

            return Task.WhenAny(Client.Close(), cancellation.Task);
        }
    }
}
