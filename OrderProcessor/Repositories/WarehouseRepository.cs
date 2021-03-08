using OrderProcessor.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderProcessor.Repositories
{
    public class WarehouseRepository : IWarehouseRepository
    {
        private readonly Dictionary<string, int> _productCounts = new Dictionary<string, int>();

        public void AddInventory(string productCode, int quantity)
        {
            if(_productCounts.ContainsKey(productCode))
            {
                _productCounts[productCode] += quantity;
            }
            else
            {
                _productCounts[productCode] = quantity;
            }
        }

        public bool CheckInventory(string productCode, int quantityNeeded)
        {
            if (!_productCounts.ContainsKey(productCode)) return false;

            var quantityAvailable = _productCounts[productCode];
            return quantityAvailable >= quantityNeeded;
        }
    }
}
