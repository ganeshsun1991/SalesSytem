using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TNSalesSystem.Domain.Card
{
    public class CardMember
    {
        public int CardId { get; set; }
        public string BiggerSmallerName { get; set; }
        public int BiggerSmallerAge { get; set; }
        public bool BiggerSmallerStatus { get; set; }
        public string Gender { get; set; }
        public string NameType { get; set; }
    }
}
