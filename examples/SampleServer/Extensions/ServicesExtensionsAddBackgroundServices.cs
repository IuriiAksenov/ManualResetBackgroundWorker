using System;
using Core.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace SampleServer.Extensions
{
    public static class ServicesExtensionsAddBackgroundServices
    {
        private const string SectionName = "BackgroundServices";

        public static IServiceCollection AddBackgroundServices<T>(this IServiceCollection services,
            string sectionName = SectionName) where T : BackgroundServicesOptions, new()
        {
            if (services is null)
            {
                throw new ArgumentNullException($"{nameof(AddBackgroundServices)}: {nameof(services)} is null.");
            }

            using var serviceProvider = services.BuildServiceProvider();
            var configuration = serviceProvider.GetRequiredService<IConfiguration>();
            services.AddOptions();
            services.Configure<T>(configuration.GetSection(SectionName));
            services.AddSingleton(configuration.GetOptions<T>(SectionName));

            return services;
        }
    }
}