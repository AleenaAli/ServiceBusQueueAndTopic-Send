using Azure.Messaging.ServiceBus;
using AzureServiceQueue;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServiceBusQueueAndTopic_Send
{
    class Program
    {
        private static string connectionStringForQueue = "Endpoint=sb://sb-ordersdemo.servicebus.windows.net/;SharedAccessKeyName=Send;SharedAccessKey=DOuoNZ47jL725wF8jqSmDl3BNv2pE5RV9qFiyZtb3A4=;EntityPath=ordersqueue";
        private static string queue_name = "ordersqueue";

        // policies for topic
        private static string connectionStringForTopic = "Endpoint=sb://sb-ordersdemo.servicebus.windows.net/;SharedAccessKeyName=Send;SharedAccessKey=e0ABuFVuXTMt5czQMHX/CR44g0TrPWnY5xcStkXKrJw=;EntityPath=orderstopic";
        private static string topic_name = "orderstopic";

        public static async Task Main(string[] args)
        {
            // Send message to Queue
            //await SendMessageToQueueAsync();


            // Send message to Topic
            await SendMessageToTopicAsync();

            Console.WriteLine("All of the messages have been sent");

        }


        private static async Task SendMessageToQueueAsync()
        {
            List<Order> _orders = new List<Order>()
            {
                new Order() {OrderID="O1",Quantity=19,UnitPrice=1.11m},
                new Order() {OrderID="O2",Quantity=19,UnitPrice=0.0m},
                new Order() {OrderID="O3",Quantity=19,UnitPrice=1.11m},
                new Order() {OrderID="O4",Quantity=19,UnitPrice=1.11m},
                new Order() {OrderID="O5",Quantity=19,UnitPrice=1.11m }
            };

            // Create a Service Bus client
            ServiceBusClient _client = new ServiceBusClient(connectionStringForQueue);


            // Creating sender for topic
            ServiceBusSender _sender = _client.CreateSender(queue_name);

            foreach (Order _order in _orders)
            {
                // Create a message that we can send
                ServiceBusMessage _message = new ServiceBusMessage(_order.ToString());
                _message.ContentType = "application/json";

                // Send the message to the queue
                await _sender.SendMessageAsync(_message);
            }

        }

        private static async Task SendMessageToTopicAsync()
        {
            List<Order> _orders = new List<Order>()
            {
                new Order() {OrderID="O1",Quantity=19,UnitPrice=9.99m},
                new Order() {OrderID="O2",Quantity=19,UnitPrice=0.0m },
                new Order() {OrderID="O3",Quantity=19,UnitPrice=11.99m},
                new Order() {OrderID="O4",Quantity=19,UnitPrice=12.99m},
                new Order() {OrderID="O5",Quantity=19,UnitPrice=13.99m }
            };

            // Create a Service Bus client
            ServiceBusClient _client = new ServiceBusClient(connectionStringForTopic);

            // Uncomment this in case when sending to Topic
            // Creating sender for topic
            ServiceBusSender _sender = _client.CreateSender(topic_name);

            foreach (Order _order in _orders)
            {
                // Create a message that we can send
                ServiceBusMessage _message = new ServiceBusMessage(_order.ToString());
                _message.ContentType = "application/json";

                // Send the message to the queue
                await _sender.SendMessageAsync(_message);
            }

        }
    }
}
