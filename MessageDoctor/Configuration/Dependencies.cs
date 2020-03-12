using MessageDoctor.Configuration.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PB.Payment.EmailSender.Subscriber.Configuration.Extensions;
using System;
using System.IO;

namespace MessageDoctor.Configuration
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
                .AddTransient<MessageDoctorService, MessageDoctorService>()
                .BuildServiceProvider();

            return serviceProvider;
        }
    }
}
