using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PB.ITOps.Logging;
using System.IO;

namespace MessageDoctorTestSubscriber.Configuration.Extensions
{
    internal static class LoggingServiceCollectionExtensions
    {
        public static IServiceCollection AddLog4NetLogging(this IServiceCollection services, IConfiguration configuration)
        {
            var isDevelopment = configuration.GetValue<string>("Environment")?.ToLower().Equals("development") ?? false;

            services.AddLogging(l =>
            {
                l.AddConfiguration(configuration.GetSection("Logging"));

                if (isDevelopment)
                {
                    l.AddDebug();
                    l.AddConsole();
                }
                else
                {
                    l.AddLog4Net(Path.Combine("Configuration", "log4net.xml"));
                }
            });

            return services;
        }
    }
}
