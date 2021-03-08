using Microsoft.Extensions.Hosting;
using OrderProcessor.Interfaces;
using OrderProcessor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OrderProcessor.Service
{
    public class OrderProcessingService : IOrderProcessor
    {
        private IWarehouseRepository _warehouseRepository;
        private IPaymentProcessor _paymentProcessor;
        private IMailService _mailService;

        public OrderProcessingService(IWarehouseRepository warehouseRepository, IPaymentProcessor paymentProcessor, IMailService mailService)
        {
            _warehouseRepository = warehouseRepository;
            _paymentProcessor = paymentProcessor;
            _mailService = mailService;
        }

        public ServiceResponse ProcessOrder(OrderRequest request)
        {
            var productAvailable = _warehouseRepository.CheckInventory(request.ProductCode, request.Quantity);

            if (!productAvailable)
            {
                return new ServiceResponse
                {
                    Success = false,
                    Message = $"Insufficient inventory for item '{request.ProductCode}'."
                };
            }

            var orderTotal = request.OrderTotal;
            var paymentCharged = _paymentProcessor.ChargePayment(request.CreditCardNumber, orderTotal);

            if (!paymentCharged)
            {
                return new ServiceResponse
                {
                    Success = false,
                    Message = $"Payment method processor failed to charge credit card."
                };
            }

            _mailService.SendMail("shipping_dept@acme.com", "sales@acme.com",
                "Order To Ship",
                $"Order to customer {request.CustomerName} should be shipped to the following address:\n" +
                $"{request.ShipToAddrres}");

            return new ServiceResponse
            {
                Success = true
            };
        }
    }
}
    