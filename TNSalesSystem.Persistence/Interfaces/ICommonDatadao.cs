using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TNSalesSystem.Domain.Common;

namespace TNSalesSystem.Persistence.Interfaces
{
    public interface ICommonDatadao
    {
        bool InsertCommonData(CommonData cd);
        bool DeleteCommonData(Hashtable ht);
        bool UpdataCommonData(Hashtable ht);
    }
}
