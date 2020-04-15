using Microsoft.Extensions.DependencyInjection;
using MessageDoctorTestSubscriber.Configuration;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace MessageDoctorTestSubscriber
{
    internal static class Program
    {
        private static async Task Main()
        {
            Console.Title = Assembly.GetExecutingAssembly().ToString();
            var container = Dependencies.Install();
            Console.WriteLine("Listening...");
            var subscriber = container.GetService<Pat.Subscriber.Subscriber>();
            await subscriber.Run(null, new[] { Assembly.GetExecutingAssembly() });
        }
    }
}
