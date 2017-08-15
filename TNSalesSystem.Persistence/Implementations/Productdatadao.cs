using System;
using Dapper;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TNSalesSystem.Domain.Product_Details;
using TNSalesSystem.Persistence.Interfaces;
using System.Data.SqlClient;
using System.Data;

namespace TNSalesSystem.Persistence.Implementations
{
    public class Productdatadao : IProductDatadao
    {
        static string constring = System.Configuration.ConfigurationManager.ConnectionStrings["TNSalesSystem"].ConnectionString;
        SqlConnection cn = new SqlConnection(constring);

        public bool InsertProductData(ProductDetails objPD)
        {
            bool res = false;

            try
            {
                var sql = "[dbo].[InsertProductData]";
                DynamicParameters param = new DynamicParameters();
                param.Add("ProductName", objPD.ProductName);
                param.Add("ProductType", objPD.ProductType);
                param.Add("Unit", objPD.Unit);
                param.Add("ProductRate", objPD.ProductRate);
                param.Add("Descrption", objPD.Descrption);
                param.Add("CreatedDate", objPD.CreatedDate);
                
                cn.Open();
                res = Convert.ToBoolean(cn.Execute(sql, param, commandType: CommandType.StoredProcedure));
                cn.Close();


            }
            catch (Exception ex)
            {
            }
            finally
            {

            }
            return res;
        }
    }
}
