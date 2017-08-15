using System;
using Dapper;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TNSalesSystem.Domain.Card;
using TNSalesSystem.Persistence.Interfaces;
using System.Data;

namespace TNSalesSystem.Persistence.Implementations
{
    public class CardDatadao : ICardDatadao
    {
        static string constring = System.Configuration.ConfigurationManager.ConnectionStrings["TNSalesSystem"].ConnectionString;
        SqlConnection cn = new SqlConnection(constring);

        public bool InsetCardData(string xml)
        {
            int res = 0;
            try
            {
                var sql = "[dbo].[InsertCardData]";
                DynamicParameters param = new DynamicParameters();
                param.Add("xmlparam", xml);

                cn.Open();
                res = cn.Execute(sql, param, commandType: CommandType.StoredProcedure, commandTimeout: 300);
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

        public bool UpdateUnitWiseProductAllotment(Hashtable ht)
        {
            int res = 0;
            try
            {
                var sql = "[dbo].[UnitWiseProductAllotmentUpdate]";
                DynamicParameters param = new DynamicParameters();
                param.Add("@userid", ht["userid"]);
                param.Add("@ip", ht["ip"]);
                param.Add("@id", ht["id"]);
                param.Add("@field", ht["field"]);
                param.Add("@value", ht["value"]);

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

        public dynamic GetCard(string xml)
        {
            IList<CardDetails> CardList = null;

            var sql = "[dbo].[GetCardData]";
            DynamicParameters param = new DynamicParameters();
            param.Add("xmlparam", xml);

            cn.Open();
            CardList = cn.Query<CardDetails>(sql, param, commandType: CommandType.StoredProcedure, commandTimeout: 300).ToList();
            cn.Close();



            return CardList;

        }

        public CardDetails GetIndividualCardDetails(int CardId)
        {
            CardDetails res;

            var cardsql = @"select cd.CardId,cd.CardCode,cd.IsAAYCard,cd.NoOfCylinders,cd.CityType,cd.CardHolderName,cd.MobileNo,cd.AlternateMobileNo,cd.LandLineNo,cd.Address1,
                            cd.Address2,cd.City,cd.District,cd.Taluk,cd.State,cd.Country,cd.PostalCode,cd.IsActive,crd.CreatedDate,cd.CardType,cd.CreatedBy,convert(decimal(18,2),cd.TotalUnits) as TotalUnits from dbo.Card_Details cd with(nolock)
                            inner join dbo.Card_ID crd with(nolock) on cd.CardId = crd.Id
                            where crd.Id = @CardId";

            var membersql = @"select cm.* from dbo.Card_Member cm with(nolock)
                              inner join dbo.Card_ID crd with(nolock) on cm.CardId = crd.Id
                              where crd.Id = @CardId order by cm.BiggerSmallerAge desc";


            DynamicParameters param = new DynamicParameters();
            param.Add("CardId", CardId);

            cn.Open();
            res = cn.Query<CardDetails>(cardsql, param).SingleOrDefault();
            res.CardMember = cn.Query<CardMember>(membersql, param).ToList();
            cn.Close();

            return res;

        }


    }

}