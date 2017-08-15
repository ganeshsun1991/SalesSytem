using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TNSalesSystem.Domain.Card
{
    public class UnitWiseProductAllotment
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string CardType { get; set; }
        public decimal Unit { get; set; }
        public decimal ProductQuantity { get; set; }
        public string ProductName { get; set; }
    }
}
