using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Data;
using TNSalesSystem.Domain.Common;
using TNSalesSystem.Persistence.Interfaces;
using System.Data.SqlClient;
using System.Collections;

namespace TNSalesSystem.Persistence.Implementations
{
    public class CommonDatadao : ICommonDatadao
    {
        static string constring = System.Configuration.ConfigurationManager.ConnectionStrings["TNSalesSystem"].ConnectionString;
        SqlConnection cn = new SqlConnection(constring);

        public bool InsertCommonData(CommonData cd)
        {
            int res = 0;
            try
            {
                var sql = "[dbo].[InsertCommonData]";
                DynamicParameters param = new DynamicParameters();
                param.Add("type", cd.Type);
                param.Add("name", cd.Name);
                param.Add("value", cd.Value);
                param.Add("descrption", cd.Descrption);

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
        public bool DeleteCommonData(Hashtable ht)
        {
            int res = 0;
            try
            {
                var sql = "[dbo].[CommonDataUpdateDelete]";
                DynamicParameters param = new DynamicParameters();
                param.Add("mode", ht["mode"]);
                param.Add("user", ht["user"]);
                param.Add("ip", ht["ip"]);
                param.Add("id", ht["id"]);                                            
                param.Add("field", ht["field"]);
                param.Add("value", ht["value"]);             
                
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
        public bool UpdataCommonData(Hashtable ht)
        {
            int res = 0;
            try
            {
                var sql = "[dbo].[CommonDataUpdateDelete]";
                DynamicParameters param = new DynamicParameters();
                param.Add("mode", ht["mode"]);
                param.Add("user", ht["user"]);
                param.Add("ip", ht["ip"]);
                param.Add("id", ht["id"]);
                param.Add("field", ht["field"]);
                param.Add("value", ht["value"]);

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
