using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TNSalesSystem.Domain.Shop
{
    public class ShopData
    {
        public int Id { get; set; }
        public string ShopNo { get; set; }
        public string ShopName { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public int District { get; set; }
        public int State { get; set; }
        public int Country { get; set; }
        public int Taluk { get; set; }
        public string PostalCode { get; set; }
        public DateTime SavedData { get; set; }
        public bool IsDeleted { get; set; }

    }
}
