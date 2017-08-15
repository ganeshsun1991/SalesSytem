using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace TNSalesSystem.Domain.Menu
{
    public class LeftMainMenu
    {
        public int MenuId { get; set; }
        public string MenuName { get; set; }
        public string MainURLLink { get; set; }
        public int OrderBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public IList<LeftSubMenu> LeftSubMenuList { get; set; }
    }
}
