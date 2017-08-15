using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TNSalesSystem.Domain.Common
{
    public class CommonData
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public DateTime SavedDate { get; set; }
        public string Descrption { get; set; }
        public bool IsDeleted { get; set; }
        public int TotalCount { get; set; }
    }
}
