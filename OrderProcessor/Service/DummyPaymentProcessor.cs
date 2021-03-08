using OrderProcessor.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderProcessor.Service
{
    public class DummyPaymentProcessor : IPaymentProcessor
    {
        public bool ChargePayment(string creditCardNumber, decimal amount)
        {
            // Dummy implementation
            if(creditCardNumber == "1234-5678-9012-3456")
                return true;

            return false;
        }
    }
}
