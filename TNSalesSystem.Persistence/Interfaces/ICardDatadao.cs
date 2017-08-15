using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using TNSalesSystem.Domain.Card;

namespace TNSalesSystem.Persistence.Interfaces
{
    public interface ICardDatadao
    {
        bool InsetCardData(string xml);
        bool UpdateUnitWiseProductAllotment(Hashtable ht);
        dynamic GetCard(string xml);
        CardDetails GetIndividualCardDetails(int CardId);
    }
}
