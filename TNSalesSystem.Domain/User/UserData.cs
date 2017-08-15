using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
namespace TNSalesSystem.Domain.User
{
    public class UserData
    {
        public int UserID { get; set; }        
        public string UserName { get; set; }
        public string MailID { get; set; }
        public string MobileNo { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public int District { get; set; }
        public int State { get; set; }
        public int Country { get; set; }
        public string DistrictName { get; set; }
        public string StateName { get; set; }
        public string CountryName { get; set; }
        public int Taluk { get; set; }
        public string PostalCode { get; set; }
        public IList<int> RoleId { get; set; }
        public string LoginName { get; set; }
        public string Password { get; set; }
        public string AlternateMobileNo { get; set; }
        public DateTime DOJ { get; set; }
        public Int64 Row_Id { get; set; }
    }
}
