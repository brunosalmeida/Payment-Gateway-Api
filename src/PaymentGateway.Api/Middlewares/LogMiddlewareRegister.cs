using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using PaymentGateway.Infrastructure.Dispatch;
using RabbitMQ.Client;

namespace PaymentGateway.Api.Middlewares
{
    public static class LogMiddlewareRegister
    {
        public static void UseEnterpriseLog(this IApplicationBuilder app)
        {
            app.UseMiddleware<LogMiddleware>();
        }

        public static void AddEnterpriseLog(this IServiceCollection services)
        {
            services.AddTransient<DispatchService>();

            services.AddSingleton<IConnection>(sp => sp.GetRequiredService<ConnectionFactory>().CreateConnection());

            services.AddScoped<IModel>(sp => sp.GetRequiredService<IConnection>().CreateModel());
        }
    }
}