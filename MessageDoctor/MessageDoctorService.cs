using MessageDoctor.Contract;
using Pat.Sender;
using System;
using System.Threading.Tasks;

namespace MessageDoctor
{
    public class MessageDoctorService
    {
        private readonly IMessagePublisher _messagePublisher;

        public MessageDoctorService(IMessagePublisher messagePublisher)
        {
            _messagePublisher = messagePublisher;
        }

        public async Task PerformMagic()
        {
            var messageToPublish = new TestMessageV1
            {
                Id = Guid.NewGuid().ToString()
            };

            await _messagePublisher.PublishEvent(messageToPublish);
            //await _messagePublisher.ScheduleCommand(messageToPublish, "messages", DateTime.UtcNow);
        }
    }
}
