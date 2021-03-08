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
                            .AddScoped<IMailService, PrintToConsoleMailService>()
                            .AddScoped<IPaymentProcessor, DummyPaymentProcessor>()
                            .AddScoped<IWarehouseRepository, WarehouseRepository>()
                            .AddScoped<IOrderProcessor, OrderProcessingService>());
    }

    public class OrderProcessorRunner : BackgroundService
    {
        private readonly IOrderProcessor _orderProcessor;
        private readonly IWarehouseRepository _warehouseRepository;

        public OrderProcessorRunner(IOrderProcessor orderProcessor, IWarehouseRepository warehouseRepository) =>
            (_orderProcessor, _warehouseRepository) = (orderProcessor, warehouseRepository);

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _warehouseRepository.AddInventory("RGB100", 2);

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
                ProductCode = "RGB100",
                Quantity = 1,
                UnitPrice = 19.99m,
                CreditCardNumber = "1234-5678-9012-3456"
            };

            var result = _orderProcessor.ProcessOrder(request);

            Console.WriteLine(result.Message);

            return Task.CompletedTask;
        }
    }
}
