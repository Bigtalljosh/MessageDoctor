using MessageDoctor.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace MessageDoctor
{
    internal static class Program
    {
        private static async Task Main()
        {
            Console.Title = Assembly.GetExecutingAssembly().ToString();
            var container = Dependencies.Install();
            Console.WriteLine("Starting...");

            var doctor = container.GetService<MessageDoctorService>();
            await doctor.PerformMagic();
        }
    }
}
