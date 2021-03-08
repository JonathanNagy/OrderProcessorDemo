using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderProcessor.Interfaces
{
    public interface IWarehouseRepository
    {
        void AddInventory(string productCode, int quantity);
        bool CheckInventory(string productCode, int quantityNeeded);
    }
}
