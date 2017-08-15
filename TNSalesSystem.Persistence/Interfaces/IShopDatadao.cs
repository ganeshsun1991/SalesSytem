using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TNSalesSystem.Domain.Shop;

namespace TNSalesSystem.Persistence.Interfaces
{
    public interface IShopDatadao
    {
        bool InsertShopData(ShopData objSD);
    }
}
