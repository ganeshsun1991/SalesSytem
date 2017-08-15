using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TNSalesSystem.Domain;
using TNSalesSystem.Domain.Card;
namespace TNSalesSystem.Service.Interfaces
{
    public interface ICardService
    {
        CardDetails CalculateUnitAllocation(CardDetails objCD);
    }
}
