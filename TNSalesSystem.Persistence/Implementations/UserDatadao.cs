using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TNSalesSystem.Persistence.Interfaces;
using TNSalesSystem.Domain.User;
using System.Data.SqlClient;
using Dapper;
using System.Data;

namespace TNSalesSystem.Persistence.Implementations
{
    public class UserDatadao : IUserDatadao
    {
        static string constring = System.Configuration.ConfigurationManager.ConnectionStrings["TNSalesSystem"].ConnectionString;
        SqlConnection cn = new SqlConnection(constring);


        public bool InsertUserData(UserData objUD)
        {
            bool res = false;
            try
            {
                var sql = "dbo.[InsertUserData]";
                DynamicParameters param = new DynamicParameters();
                param.Add("LoginName", objUD.LoginName);
                param.Add("Password", objUD.Password);
                param.Add("UserName", objUD.UserName);
                param.Add("MailID", objUD.MailID);
                param.Add("MobileNo", objUD.MobileNo);
                param.Add("AlternateMobileNo", objUD.AlternateMobileNo);
                param.Add("Address1", objUD.Address1);
                param.Add("Addres2", objUD.Address2);
                param.Add("City", objUD.City);
                param.Add("District", objUD.District);
                param.Add("State", objUD.State);
                param.Add("Country", objUD.Country);
                param.Add("PostalCode", objUD.PostalCode);
                param.Add("RoleID ", string.Join(",", objUD.RoleId));

                cn.Open();
                cn.Execute(sql, param, commandType: CommandType.StoredProcedure);
                cn.Close();


            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                cn.Close();
            }
            return res;

        }
        public bool CheckLoginnameAvaliablity(string Loginname)
        {
            int count = 0;
            bool res = false;
            try
            {
                var sql = "select count(*) from dbo.[User] where LoginName = @Loginname";
                DynamicParameters param = new DynamicParameters();
                param.Add("Loginname", Loginname);

                cn.Open();
                count = cn.Query<int>(sql, param).SingleOrDefault();
                cn.Close();

                if (count > 0)
                {
                    res = true;
                }
                else
                {
                    res = false;
                }

            }
            catch (Exception ex)
            {

            }
            finally
            {

            }
            return res;
        }
        public List<UserData> GetUserList()
        {
            List<UserData> res = new List<UserData>();            
            try
            {
                var sql = "[dbo].[GetUserList]";

                cn.Open();
                res = cn.Query<UserData>(sql, commandType: CommandType.StoredProcedure).ToList();
                cn.Close();


            }
            catch (Exception ex)
            {

            }
            finally
            {
                cn.Close();
            }
            return res;
        }

        public IList<UserData> GetUserList(string Mode, int Starterow, int Endrow, out int totalItems)
        {
            IList<UserData> res = null;
            totalItems = 0;
            var sql = "[dbo].[GetUserData]";
            DynamicParameters param = new DynamicParameters();
            param.Add("@Mode", Mode);
            param.Add("@Starterow", Starterow);
            param.Add("@Endrow", Endrow);
            param.Add("@totalItems", totalItems, DbType.Int32, ParameterDirection.Output);

            cn.Open();
            res = cn.Query<UserData>(sql, param, commandType: CommandType.StoredProcedure).ToList();
            totalItems = param.Get<Int32>("totalItems");
            cn.Close();

            return res;
        }

    }

}