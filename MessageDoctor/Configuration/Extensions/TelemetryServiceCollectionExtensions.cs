using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pat.Subscriber.Telemetry.StatsD;

namespace MessageDoctor.Configuration.Extensions
{
    internal static class TelemetryServiceCollectionExtensions
    {
        public static IServiceCollection AddStatsDTelemetry(this IServiceCollection services, IConfiguration configuration)
        {
            var statsDConfig = new StatisticsReporterConfiguration();
            configuration.GetSection("StatsD").Bind(statsDConfig);
            services.AddTransient<IStatisticsReporter, StatisticsReporter>()
                    .AddSingleton(statsDConfig);

            return services;
        }
    }
}
