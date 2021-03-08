using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderProcessor.Interfaces
{
    public interface IPaymentProcessor
    {
        Boolean ChargePayment(string creditCardNumber, decimal amount);
    }
}
