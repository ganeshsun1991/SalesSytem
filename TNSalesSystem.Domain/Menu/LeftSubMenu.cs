using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TNSalesSystem.Domain.Menu
{
    public class LeftSubMenu
    {
        public int SubMenuId { get; set; }
        public string SubMenuName { get; set; }
        public int MainMenuId { get; set; }
        public string SubURLLink { get; set; }
        public int OrderBy { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
