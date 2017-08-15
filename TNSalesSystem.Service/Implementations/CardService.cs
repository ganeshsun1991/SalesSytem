using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TNSalesSystem.Domain.Card;
using TNSalesSystem.Service.Interfaces;

namespace TNSalesSystem.Service.Implementations
{
    public class CardService : ICardService
    {
        public CardDetails CalculateUnitAllocation(CardDetails objCD)
        {
            objCD.BiggersCount = objCD.BiggerName.Count();
            objCD.SmallersCount = objCD.SmallerName != null ? objCD.SmallerName.Count(): 0;


            objCD.TotalUnits = (decimal)(objCD.BiggersCount + (objCD.SmallersCount == 0 ? 0 : ((decimal)(objCD.SmallerName.Count() - (decimal)objCD.SmallerName.Count() / 2))));


            return objCD;
        }
    }
}
