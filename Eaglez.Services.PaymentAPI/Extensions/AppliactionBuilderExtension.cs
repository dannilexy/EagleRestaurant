using Eaglez.Services.PaymentAPI.Messaging;
using Microsoft.Extensions.DependencyInjection;

namespace Eaglez.Services.PaymentAPI.Extensions
{
    public static class AppliactionBuilderExtension
    {
        public static IAzureMessageBusConsumer ServiceBusConsumer { get; set; }
        public static IServiceScopeFactory _scopeFactory { get; set; }

        public static IApplicationBuilder UseAzureServiceBusConsumer(this IApplicationBuilder app)
        {
            //ServiceBusConsumer = app.ApplicationServices.GetService<IAzureMessageBusConsumer>();

            _scopeFactory = app.ApplicationServices.GetService<IServiceScopeFactory>();
            using var scope = _scopeFactory.CreateScope();

            ServiceBusConsumer = scope.ServiceProvider.GetRequiredService<IAzureMessageBusConsumer>();

            var hostApplicationLife = app.ApplicationServices.GetService<IHostApplicationLifetime>();
            hostApplicationLife.ApplicationStarted.Register(OnStart);
            hostApplicationLife.ApplicationStopped.Register(OnStop);
            return app;
        }

        private static void OnStart()
        {
            ServiceBusConsumer.Start();
        }

        private static void OnStop()
        {
            ServiceBusConsumer.Stop();
        }
    }
}
