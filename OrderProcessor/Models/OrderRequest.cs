using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderProcessor.Models
{
    public class OrderRequest
    {
        public string CustomerName { get; set; }
        public Address ShipToAddrres { get; set; }
        public string ProductCode { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public string CreditCardNumber { get; set; }

        public decimal OrderTotal
        {
            get
            {
                return UnitPrice * Quantity;
            }
        }
    }
}
