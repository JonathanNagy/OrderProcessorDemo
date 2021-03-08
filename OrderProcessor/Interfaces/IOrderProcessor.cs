using OrderProcessor.Models;
using OrderProcessor.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderProcessor.Interfaces
{
    public interface IOrderProcessor
    {
        ServiceResponse ProcessOrder(OrderRequest request);
    }
}
