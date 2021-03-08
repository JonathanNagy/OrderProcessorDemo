using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using OrderProcessor.Interfaces;
using OrderProcessor.Models;
using OrderProcessor.Service;
using System;

namespace OrderProcessor.Tests
{
    [TestClass]
    public class OrderProcessorTests
    {
        private Mock<IWarehouseRepository> _mockWarehouseRepository;
        private Mock<IMailService> _mockMailService;
        private Mock<IPaymentProcessor> _mockPaymentProcessor;
        private IOrderProcessor _orderProcessingService;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockWarehouseRepository = new Mock<IWarehouseRepository>();
            _mockPaymentProcessor = new Mock<IPaymentProcessor>();
            _mockMailService = new Mock<IMailService>();
            _orderProcessingService = new OrderProcessingService(_mockWarehouseRepository.Object, _mockPaymentProcessor.Object, _mockMailService.Object);
        }

        [TestMethod]
        public void OrderProcessor_InventorySufficient_PaymentCharged()
        {
            // Arrange
            // Return inventory sufficient on CheckInventory
            _mockWarehouseRepository.Setup(x => x.CheckInventory(It.IsAny<string>(), It.IsAny<int>())).Returns(true);

            // Act
            _orderProcessingService.ProcessOrder(CreateOrderRequest());

            // Assert
            // Payment processor should be called once
            _mockPaymentProcessor.Verify(x => x.ChargePayment(It.IsAny<string>(), It.IsAny<decimal>()), Times.Once);
        }

        [TestMethod]
        public void OrderProcessor_InventorySufficient_EmailSent()
        {
            // Arrange
            // Return inventory sufficient on CheckInventory
            _mockWarehouseRepository.Setup(x => x.CheckInventory(It.IsAny<string>(), It.IsAny<int>())).Returns(true);
            // Return true on ChargePayment
            _mockPaymentProcessor.Setup(x => x.ChargePayment(It.IsAny<string>(), It.IsAny<decimal>())).Returns(true);

            // Act
            _orderProcessingService.ProcessOrder(CreateOrderRequest());

            // Assert
            // Mail service should be called once
            _mockMailService.Verify(x => x.SendMail(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }

        [TestMethod]
        public void OrderProcessor_InventoryNotSufficient_PaymentNotCharged()
        {
            // Arrange
            // Return inventory not sufficient on CheckInventory
            _mockWarehouseRepository.Setup(x => x.CheckInventory(It.IsAny<string>(), It.IsAny<int>())).Returns(false);

            // Act
            _orderProcessingService.ProcessOrder(CreateOrderRequest());

            // Assert
            // Payment processor should not be called
            _mockMailService.Verify(x => x.SendMail(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Never);
        }

        [TestMethod]
        public void OrderProcessor_InventoryNotSufficient_EmailNotSent()
        {
            // Arrange
            // Return inventory not sufficient on CheckInventory
            _mockWarehouseRepository.Setup(x => x.CheckInventory(It.IsAny<string>(), It.IsAny<int>())).Returns(false);

            // Act
            _orderProcessingService.ProcessOrder(CreateOrderRequest());

            // Assert
            // Mail service should not be called
            _mockMailService.Verify(x => x.SendMail(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Never);
        }

        private OrderRequest CreateOrderRequest()
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
            return request;
        }
    }
}
