using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TNSalesSystem.Domain.User;

namespace TNSalesSystem.Persistence.Interfaces
{
    public interface IUserDatadao
    {
        bool InsertUserData(UserData objUD);
        bool CheckLoginnameAvaliablity(string Loginname);
        List<UserData> GetUserList();
        IList<UserData> GetUserList(string Mode, int Starterow, int Endrow, out int totalItems);
    }
}
