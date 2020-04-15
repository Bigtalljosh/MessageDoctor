using MessageDoctor.Contract;
using Pat.Subscriber;
using System;
using System.Threading.Tasks;

namespace MessageDoctor.TestSubscriber.Handlers
{
    public class TestMessageV1Handler : IHandleEvent<TestMessageV1>
    {
        public Task HandleAsync(TestMessageV1 message)
        {
            Console.WriteLine($"Received message with id: {message.Id}");
            return Task.CompletedTask;
        }
    }
}
