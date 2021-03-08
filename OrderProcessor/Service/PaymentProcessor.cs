using OrderProcessor.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderProcessor.Service
{
    public class PaymentProcessor : IPaymentProcessor
    {
        public bool ChargePayment(string creditCardNumber, decimal amount)
        {
            throw new NotImplementedException();
        }
    }
}
