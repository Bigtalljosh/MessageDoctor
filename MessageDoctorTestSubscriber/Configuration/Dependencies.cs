using MessageDoctor.TestSubscriber.Handlers;
using MessageDoctorTestSubscriber.Configuration.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pat.Subscriber.NetCoreDependencyResolution;
using System;
using System.IO;

namespace MessageDoctorTestSubscriber.Configuration
{
    internal static class Dependencies
    {
        public static IServiceProvider Install()
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile(Path.Combine("Configuration", "appSettings.json"))
                .Build();

            var serviceProvider = new ServiceCollection()
                .AddLog4NetLogging(configuration)
                .AddBasePatLiteServices(configuration)
                .AddStatsDTelemetry(configuration)
                .AddHandlersFromAssemblyContainingType<TestMessageV1Handler>()
                .BuildServiceProvider();

            return serviceProvider;
        }
    }
}
