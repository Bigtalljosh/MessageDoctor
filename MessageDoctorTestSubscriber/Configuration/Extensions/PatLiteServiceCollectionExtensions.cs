using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pat.DataProtection;
using Pat.Sender;
using Pat.Sender.Correlation;
using Pat.Sender.DataProtectionEncryption;
using Pat.Sender.MessageGeneration;
using Pat.Sender.NetCoreLog;
using Pat.Subscriber;
using Pat.Subscriber.NetCoreDependencyResolution;
using System;
using System.Collections.Generic;

namespace MessageDoctorTestSubscriber.Configuration.Extensions
{
    public static class PatLiteServiceCollectionExtensions
    {
        public static IServiceCollection AddBasePatLiteServices(this IServiceCollection services, IConfiguration configuration)
        {
            var senderSettings = new PatSenderSettings();
            var subscriberConfiguration = new SubscriberConfiguration();
            var dataProtectionConfiguration = new DataProtectionConfiguration();

            configuration.GetSection("PatLite:Sender").Bind(senderSettings);
            configuration.GetSection("PatLite:Subscriber").Bind(subscriberConfiguration);
            configuration.GetSection("DataProtection").Bind(dataProtectionConfiguration);

            services.AddPatLite(subscriberConfiguration)
                .AddTransient<IEncryptedMessagePublisher>(
                    provider => new EncryptedMessagePublisher(
                        provider.GetRequiredService<IMessageSender>(),
                        dataProtectionConfiguration,
                        provider.GetRequiredService<MessageProperties>()))
                    .AddPatSenderNetCoreLogAdapter()
                    .AddTransient<IMessageSender, MessageSender>()
                    .AddSingleton<IMessageGenerator, MessageGenerator>()
                    .AddSingleton<MessageProperties, MessageProperties>()
                    .AddSingleton(senderSettings)
                    .AddSingleton<ICorrelationIdProvider, NewCorrelationIdProvider>()
                    .AddTransient<IMessagePublisher>(provider => new MessagePublisher(
                            provider.GetRequiredService<IMessageSender>(),
                            provider.GetRequiredService<IMessageGenerator>(),
                            GetAnnotatedMessageProperties(provider)
                        ));

            return services;
        }

        private static MessageProperties GetAnnotatedMessageProperties(IServiceProvider context)
        {
            var messageContext = context.GetService<MessageContext>();
            var messageProperties = new MessageProperties(messageContext.CorrelationId);

            if (messageContext.Synthetic)
            {
                messageProperties.CustomProperties = new Dictionary<string, string>
                {
                    {"Synthetic", messageContext.Synthetic.ToString()},
                    {"DomainUnderTest", messageContext.DomainUnderTest}
                };
            }

            return messageProperties;
        }
    }
}
