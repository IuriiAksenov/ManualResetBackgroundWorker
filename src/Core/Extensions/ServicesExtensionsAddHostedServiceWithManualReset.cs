using System;

using Microsoft.Extensions.DependencyInjection;

namespace Core.Extensions
{
    public static class ServicesExtensionsAddHostedServiceWithManualReset
    {
        public static IServiceCollection AddHostedServiceWithManualReset<T>(this IServiceCollection services)
            where T : ManualRestartBackgroundService
        {
            if (services is null)
            {
                throw new ArgumentNullException(
                    $"{nameof(AddHostedServiceWithManualReset)}: {nameof(services)} is null.");
            }

            services.AddSingleton<T>();
            services.AddHostedService(provider => provider.GetRequiredService<T>());

            return services;
        }
    }
}