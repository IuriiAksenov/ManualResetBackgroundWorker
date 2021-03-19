using System;
using System.Threading;
using System.Threading.Tasks;
using Core;
using Microsoft.Extensions.Logging;
using SampleServer.Extensions;

namespace SampleServer.BackgroundServices
{
    public class SampleBackgroundWorker : ManualRestartBackgroundService
    {
        private readonly TimeSpan _interval;
        private readonly ILogger<SampleBackgroundWorker> _logger;
        private readonly TimeSpan _runFrom;
        private readonly TimeSpan _runTo;


        public SampleBackgroundWorker(ILogger<SampleBackgroundWorker> logger,
            SampleServerBackgroundServicesOptions backgroundServicesOptions) : base(logger)
        {
            logger.LogInformation("ctr: Begin.");
            _logger = logger;
            _interval = TimeSpan.FromSeconds(backgroundServicesOptions.SampleBackgroundWorker.Interval);
            _runTo = backgroundServicesOptions.SampleBackgroundWorker.RunTo;
            _runFrom = backgroundServicesOptions.SampleBackgroundWorker.RunFrom;
            logger.LogInformation("ctr: End.");
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation($"{nameof(ExecuteAsync)}: Begin. Background task is starting.");

            cancellationToken.Register(() =>
                _logger.LogInformation($"{nameof(ExecuteAsync)}: background task is stopping."));

            while (Math.Abs(_interval.TotalSeconds) > 0.1 && !cancellationToken.IsCancellationRequested)
            {
                _logger.LogInformation($"{nameof(ExecuteAsync)}: task doing background work.");

                // Wait until right time. 
                // For example, the worker has to sart at 18:00, but know is 14:00. There is a wait for 4 hours.
                var timeToRun = DateTime.UtcNow.GetTimeToRangeBorder(_runFrom, _runTo);

                await ManualDelay(timeToRun, cancellationToken);

                await DoJobSampleAsync();

                await ManualDelay(_interval, cancellationToken);
            }

            _logger.LogInformation($"{nameof(ExecuteAsync)}: End. Background task was stopped.");
        }

        private static async Task DoJobSampleAsync()
        {
            for (var i = 0; i < 10; i++)
            {
                Console.WriteLine(i);
                await Task.Delay(100);
            }
        }
    }
}