namespace HustleAddiction.Platform.CalendarApi.Presentation.WebAPI.Tools.RabbitMqService
{
    using Microsoft.Extensions.Options;
    using RabbitMQ.Client;
    using RabbitMQ.Client.Events;
    using System.Text;

    public sealed class UserCreatedConsumer : BackgroundService
    {
        private readonly ConnectionFactory factory;
        private readonly RabbitMqOptions options;

        public UserCreatedConsumer(ConnectionFactory newFactory, IOptions<RabbitMqOptions> newOptions)
        {
            factory = newFactory;
            options = newOptions.Value;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var connection = factory.CreateConnection("calendar-consumer");
            var channel = connection.CreateModel();

            channel.QueueDeclare(
                options.Queue,
                durable: true,
                exclusive: false,
                autoDelete: false);

            channel.QueueBind(
                options.Queue,
                options.Exchange,
                options.RoutingKey);

            var consumer = new AsyncEventingBasicConsumer(channel);
            consumer.Received += async (_, ea) =>
            {
                var json = Encoding.UTF8.GetString(ea.Body.ToArray());
                Console.WriteLine($"[Calendar] Received: {json}");
                channel.BasicAck(ea.DeliveryTag, false);
            };

            channel.BasicConsume(
                queue: options.Queue,
                autoAck: false,
                consumer: consumer);

            return Task.CompletedTask;
        }
    }
}
