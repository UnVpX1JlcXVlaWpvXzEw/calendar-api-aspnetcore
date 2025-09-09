namespace HustleAddiction.Platform.CalendarApi.Presentation.WebAPI.Tools.RabbitMqService
{
    using Microsoft.Extensions.Options;
    using RabbitMQ.Client;

    public static class RabbitMqServiceRegistrar
    {
        public static void Register(IServiceCollection services, IConfiguration config)
        {
            services.Configure<RabbitMqOptions>(config.GetSection("RabbitMQ"));

            services.AddSingleton(provider =>
            {
                var options = provider.GetRequiredService<IOptions<RabbitMqOptions>>().Value;
                return new ConnectionFactory
                {
                    HostName = options.HostName,
                    UserName = options.UserName,
                    Password = options.Password,
                    DispatchConsumersAsync = true
                };
            });

            services.AddHostedService<UserCreatedConsumer>();
        }
    }
}
