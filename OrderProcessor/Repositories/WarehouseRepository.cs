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
        public void AddInventory(string productCode, int quantity)
        {
            throw new NotImplementedException();
        }

        public bool CheckInventory(string productCode, int quantityNeeded)
        {
            throw new NotImplementedException();
        }
    }
}
