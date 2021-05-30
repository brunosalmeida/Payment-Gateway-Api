using System.Text;
using RabbitMQ.Client;

namespace PaymentGateway.Infrastructure.Dispatch
{
    public class DispatchService : IDispatchService
    {
        private readonly IModel _model;
        public DispatchService(IModel model)
        {
            _model = model;
        }

        public void Dispatch(LogEntry log, string queueToSend)
        {
            string serializedContent = System.Text.Json.JsonSerializer.Serialize(log);

            var props = _model.CreateBasicProperties();
            //props.Persistent = true;
            props.DeliveryMode = 2;

            byte[] payload = Encoding.UTF8.GetBytes(serializedContent);

            _model.BasicPublish(string.Empty, queueToSend, props, payload);
        }

        public void Dispose()
        {
        }
    }

    public interface IDispatchService
    {
        void Dispatch(LogEntry log, string queueToSend);
        void Dispose();
    }
}