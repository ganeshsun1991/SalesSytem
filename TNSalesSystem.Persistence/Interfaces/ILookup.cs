using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TNSalesSystem.Domain.Common;
using System.Collections;
using TNSalesSystem.Domain.Product_Details;
using TNSalesSystem.Domain.Card;
using TNSalesSystem.Domain.Menu;

namespace TNSalesSystem.Persistence.Interfaces
{
    public interface ILookup
    {
        IList<CommonData> GetCommonData(Hashtable ht);
        IList<CommonData> GetDropDownData(string type = "");
        IList<ProductDetails> GetProductDetailsData();
        string GetShopName(string city = "");
        IList<UnitWiseProductAllotment> GetUnitWiseProductAllotmentData();
        IList<LeftMainMenu> GetMenuList(int UserId, string MenuType);
    }
}
