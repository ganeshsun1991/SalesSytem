using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using TNSalesSystem.Persistence.Interfaces;
using System.Collections;
using TNSalesSystem.Domain.Common;
using System.Data.SqlClient;
using System.Configuration;
using Dapper;
using TNSalesSystem.Domain.Product_Details;
using TNSalesSystem.Domain.Card;
using TNSalesSystem.Domain.Menu;

namespace TNSalesSystem.Persistence.Implementations
{
    public class Lookup : ILookup
    {
        static string constring = System.Configuration.ConfigurationManager.ConnectionStrings["TNSalesSystem"].ConnectionString;
        SqlConnection cn = new SqlConnection(constring);

        public IList<CommonData> GetCommonData(Hashtable ht)
        {
            IList<CommonData> res = null;
            try
            {
                var sql = "[dbo].[GetCommonData]";
                DynamicParameters param = new DynamicParameters();
                param.Add("name", ht["name"]);
                param.Add("value", ht["value"]);
                param.Add("date", ht["date"]);
                param.Add("type", ht["type"]);
                param.Add("startrow", ht["startrow"]);
                param.Add("endrow", ht["endrow"]);

                cn.Open();
                res = cn.Query<CommonData>(sql, param, commandType: CommandType.StoredProcedure).ToList();
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
        public IList<CommonData> GetDropDownData(string type = "")
        {
            IList<CommonData> res = null;
            try
            {
                var sql = "[dbo].[GetDropDownData]";
                DynamicParameters param = new DynamicParameters();
                param.Add("type", type);

                cn.Open();
                res = cn.Query<CommonData>(sql, param, commandType: CommandType.StoredProcedure).ToList();
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
        public string GetShopName(string city = "")
        {
            string res = "";
            try
            {
                var sql = "select top 1 @city + ' ' + convert(varchar(500),(select case when count(*) > 0 then count(*) else 1 end as [count] from  [dbo].[Shop_Details] with(nolock) where City =  @city)) from [dbo].[Shop_Details] with(nolock)";
                DynamicParameters param = new DynamicParameters();
                param.Add("city", city);

                cn.Open();
                res = cn.Query<string>(sql, param).SingleOrDefault();
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
        public IList<ProductDetails> GetProductDetailsData()
        {
            IList<ProductDetails> res = null;
            try
            {
                var sql = "select * from dbo.Product_Details with(nolock)";
                cn.Open();
                res = cn.Query<ProductDetails>(sql).ToList();
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
        public IList<UnitWiseProductAllotment> GetUnitWiseProductAllotmentData()
        {
            IList<UnitWiseProductAllotment> res = null;
            try
            {
                var sql = @"select uw.Id,uw.ProductId,isnull(cd.Name ,uw.CardType) CardType,uw.Unit,uw.ProductQuantity,pd.ProductName from dbo.Unit_Wise_Product_Allotment uw with(nolock)
                            inner join dbo.Product_Details pd with(nolock) on pd.id = uw.ProductId
                            left outer join dbo.Common_Data cd with(nolock) on convert(varchar(10),cd.Id) = uw.CardType";
                cn.Open();
                res = cn.Query<UnitWiseProductAllotment>(sql).ToList();
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
        public IList<LeftMainMenu> GetMenuList(int UserId, string MenuType)
        {
            IList<LeftMainMenu> res = new List<LeftMainMenu>();
            IList<LeftMainMenu> tempres = null;
            LeftMainMenu Menu = new LeftMainMenu();
            var sql = "[dbo].[GetMenuList]";
            DynamicParameters param = new DynamicParameters();
            param.Add("@UserId", UserId);
            param.Add("@MenuType", MenuType);

            cn.Open();
            tempres = cn.Query<LeftMainMenu>(sql, param, commandType: CommandType.StoredProcedure).ToList();
            param.Add("MenuType", "sub");
            Menu.LeftSubMenuList = cn.Query<LeftSubMenu>(sql, param, commandType: CommandType.StoredProcedure).ToList();
            cn.Close();

            if (tempres != null)
            {
                foreach (LeftMainMenu temp in tempres)
                {
                    temp.LeftSubMenuList = new List<LeftSubMenu>();
                    foreach (LeftSubMenu temps in Menu.LeftSubMenuList)
                    {
                        if (temps != null)
                        {
                            temp.LeftSubMenuList.Add(temps);
                        }
                    }
                    res.Add(temp);
                }
            }

            return res;
        }

    }
}
