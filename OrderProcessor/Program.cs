using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OrderProcessor.Interfaces;
using OrderProcessor.Models;
using OrderProcessor.Repositories;
using OrderProcessor.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OrderProcessor
{
    class Program
    {
        static void Main(string[] args) =>
            CreateHostBuilder(args).Build().Run();

        static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((_, services) =>
                    services.AddHostedService<OrderProcessorRunner>()
                            .AddScoped<IMailService, MailService>()
                            .AddScoped<IWarehouseRepository, WarehouseRepository>()
                            .AddScoped<IOrderProcessor, OrderProcessingService>());
    }

    public class OrderProcessorRunner : BackgroundService
    {
        private readonly IOrderProcessor _orderProcessor;

        public OrderProcessorRunner(IOrderProcessor orderProcessor) =>
            _orderProcessor = orderProcessor;

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var request = new OrderRequest()
            {
                CustomerName = "John Doe",
                ShipToAddrres = new Address()
                {
                    AddressLine1 = "1234 Main Street",
                    AddressLine2 = "Apt 103",
                    City = "Baltimore",
                    State = "MD",
                    PostalCode = "21231",
                    Country = "US"
                },
                CreditCardNumber = "1234-5678-9012-3456"
            };

            var result = _orderProcessor.ProcessOrder(request);

            Console.WriteLine(result.Message);

            return Task.CompletedTask;
        }
    }
}
