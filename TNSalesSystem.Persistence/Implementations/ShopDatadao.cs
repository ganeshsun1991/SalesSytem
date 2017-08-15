using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TNSalesSystem.Domain.Shop;
using Dapper;
using System.Data.SqlClient;
using TNSalesSystem.Persistence.Interfaces;
using System.Data;
namespace TNSalesSystem.Persistence.Implementations
{
    public class ShopDatadao : IShopDatadao
    {
        static string constring = System.Configuration.ConfigurationManager.ConnectionStrings["TNSalesSystem"].ConnectionString;
        SqlConnection cn = new SqlConnection(constring);

        public bool InsertShopData(ShopData objSD)
        {
            int res = 0;
            try
            {
               
                //            var sql = @"INSERT INTO [dbo].[Shop_Details] (ShopNo,ShopName,Address1,Address2,City,District,Taluk,State,Country,PostalCode,SavedData,IsDeleted)
                //                     VALUES(@ShopNo,@ShopName,@Address1,@Address2,@City,@District,@Taluk,@State,@Country,@PostalCode,Getdate(),@IsDeleted)";
                var sql = "[dbo].[InsertShopData]";
                DynamicParameters param = new DynamicParameters();

                param.Add("ShopName", objSD.ShopName);
                param.Add("Address1", objSD.Address1);
                param.Add("Address2", objSD.Address2);
                param.Add("City", objSD.City);
                param.Add("District", objSD.District);
                param.Add("Taluk", objSD.Taluk);
                param.Add("State", objSD.State);
                param.Add("Country", objSD.Country);
                param.Add("PostalCode", objSD.PostalCode);

                cn.Open();
                res = cn.Execute(sql, param, commandType: CommandType.StoredProcedure);
                cn.Close();

              
            }
            catch (Exception ex)
            {

            }
            finally
            {
                cn.Close();
            }

            return Convert.ToBoolean(res);
        }
    }
}
