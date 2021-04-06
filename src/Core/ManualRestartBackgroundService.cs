using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Core
{
    /// <summary>
    ///     Base class for implementing a long running <see cref="T:Microsoft.Extensions.Hosting.IHostedService" /> with
    ///     restart ability.
    /// </summary>
    public abstract class ManualRestartBackgroundService : BackgroundService
    {
        private readonly ILogger<ManualRestartBackgroundService> _logger;
        private CancellationTokenSource _cancelTokenSource;
        private CancellationToken _token;

        protected ManualRestartBackgroundService(ILogger<ManualRestartBackgroundService> logger)
        {
            _logger = logger;
            (_cancelTokenSource, _token) = CreateToken();
        }

        /// <summary>
        ///     Cancel pending execution
        /// </summary>
        /// <returns>True - background worker restarted / False - in another case</returns>
        public bool TryReleaseDelay()
        {
            try
            {
                _cancelTokenSource.Cancel();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return false;
            }
        }

        /// <summary>
        ///     Creates a cancellable task that completes after a specifiendnumber of milliseconds.
        ///     Task can be canceled in any time by calling <see cref="TryReleaseDelay()" />
        /// </summary>
        /// <param name="delay">The time span to wait before completing the return task</param>
        /// <returns>A task that represents the time delay.</returns>
        protected async Task ManualDelay(TimeSpan delay)
        {
            await ManualDelay(delay, default);
        }

        /// <summary>
        ///     Creates a cancellable task that completes after a specifiendnumber of milliseconds.
        ///     Task can be canceled in any time by calling <see cref="TryReleaseDelay()" />
        /// </summary>
        /// <param name="delay">The time span to wait before completing the return task</param>
        /// <param name="cancellationToken">A cancellation token to observe while waiting for the task to complete.</param>
        /// <returns>A task that represents the time delay.</returns>
        protected async Task ManualDelay(TimeSpan delay, CancellationToken cancellationToken)
        {
            var totalMilliseconds = (long) delay.TotalMilliseconds;
            await ManualDelay((int) totalMilliseconds, cancellationToken);
        }

        /// <summary>
        ///     Creates a cancellable task that completes after a specifiendnumber of milliseconds.
        ///     Task can be canceled in any time by calling <see cref="TryReleaseDelay()" />
        /// </summary>
        /// <param name="millisecondsDelay">
        ///     The number of millisecond to wait before completing the return task, or -1 to wait
        ///     indefinitely
        /// </param>
        /// <returns>A task that represents the time delay.</returns>
        protected async Task ManualDelay(int millisecondsDelay)
        {
            await ManualDelay(millisecondsDelay, default);
        }

        /// <summary>
        ///     Creates a cancellable task that completes after a specifiendnumber of milliseconds.
        ///     Task can be canceled in any time by calling <see cref="TryReleaseDelay()" />
        /// </summary>
        /// <param name="millisecondsDelay">
        ///     The number of millisecond to wait before completing the return task, or -1 to wait
        ///     indefinitely
        /// </param>
        /// <param name="cancellationToken">A cancellation token to observe while waiting for the task to complete.</param>
        /// <returns>A task that represents the time delay.</returns>
        protected async Task ManualDelay(int millisecondsDelay, CancellationToken cancellationToken)
        {
            cancellationToken.Register(() => TryReleaseDelay());

            try
            {
                await Task.Delay(millisecondsDelay, _token);
            }
            catch (OperationCanceledException ex)
            {
                _logger.LogTrace(ex, $"{nameof(ManualDelay)}: manual delay is revoked.");
            }

            (_cancelTokenSource, _token) = CreateToken();
        }

        private (CancellationTokenSource, CancellationToken) CreateToken()
        {
            var cancelTokenSource = new CancellationTokenSource();
            var token = cancelTokenSource.Token;
            token.Register(() => _logger.LogDebug($"{nameof(ManualDelay)}: manual delay is revoked."));
            return (cancelTokenSource, token);
        }

        /// <summary>
        ///     Release all resources used by the current instance of the <see cref="CancellationTokenSource" /> class.
        /// </summary>
        public override void Dispose()
        {
            _cancelTokenSource.Cancel();
            base.Dispose();
        }
    }
}