using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TNSalesSystem.Domain.Card;
using TNSalesSystem.Domain.Common;
using TNSalesSystem.Persistence.Implementations;
using TNSalesSystem.Persistence.Interfaces;
using TNSalesSystem.Service.Implementations;
using TNSalesSystem.Service.Interfaces;

namespace TNSalesSystem.Controllers.Card
{
    public class CardController : Controller
    {
        //
        // GET: /Card/
        ILookup lookup = new Lookup();
        ICardDatadao carddatadao = new CardDatadao();
        ICardService cardservice = new CardService();


        #region PrivateFunction
        private String RenderViewToString(ControllerContext context, String viewPath, object model = null)
        {
            ViewBag.Model = model;
            using (var sw = new StringWriter())
            {
                var viewResult = ViewEngines.Engines.FindView(context, viewPath, null);
                var viewContext = new ViewContext(context, viewResult.View, context.Controller.ViewData, context.Controller.TempData, sw);
                viewResult.View.Render(viewContext, sw);
                viewResult.ViewEngine.ReleaseView(context, viewResult.View);
                return sw.GetStringBuilder().ToString();
            }
        }

        private void Registerlookup()
        {
            CommonData dummy = new CommonData
           {
               Name = "-Select-",
               Id = 0
           };
            #region GenderTypeList
            List<CommonData> GenderTypeList = new List<CommonData>();
            GenderTypeList.Add(new CommonData { Name = "Male", Value = "Male" });
            GenderTypeList.Add(new CommonData { Name = "Female", Value = "Female" });
            GenderTypeList.Add(new CommonData { Name = "Other", Value = "Other" });
            #endregion


            ViewBag.GenderTypes = GenderTypeList;

            IList<CommonData> CityTypeList = lookup.GetDropDownData("City Type");
            CityTypeList.Insert(0, dummy);
            SelectList CityTypes = new SelectList(CityTypeList, "Id", "Name");
            ViewBag.CityTypes = CityTypes;

            IList<CommonData> CountryList = lookup.GetDropDownData("Country");
            //CountryList.Insert(0, dummy);
            SelectList Coutries = new SelectList(CountryList, "Id", "Name");
            ViewBag.Coutries = Coutries;

            IList<CommonData> StateList = lookup.GetDropDownData("State");
            //StateList.Insert(0, dummy);
            SelectList States = new SelectList(StateList, "Id", "Name");
            ViewBag.States = States;

            IList<CommonData> DistrictList = lookup.GetDropDownData("District");
            DistrictList.Insert(0, dummy);
            SelectList Districts = new SelectList(DistrictList, "Id", "Name");
            ViewBag.Districts = Districts;

            IList<CommonData> TalukList = lookup.GetDropDownData("Taluk");
            TalukList.Insert(0, dummy);
            SelectList Taluks = new SelectList(TalukList, "Id", "Name");
            ViewBag.Taluks = Taluks;

            IList<CommonData> CardTypeList = lookup.GetDropDownData("Card Type");
            //CardList.Insert(0, dummy);
            SelectList CardTypes = new SelectList(CardTypeList, "Id", "Name");
            ViewBag.CardTypes = CardTypes;
        }
        #endregion

        #region InsertEditCard
        public ActionResult AddCard()
        {
            Registerlookup();
            ViewBag.Frompage = "AddCard";
            return View();
        }

        public ActionResult EditCard(int CardId)
        {
            Registerlookup();

            CardDetails objCD = carddatadao.GetIndividualCardDetails(CardId);

            ViewBag.CardMember = objCD.CardMember;
            ViewBag.Frompage = "EditCard";
            ViewBag.CardId = CardId;
            return View("AddCard", objCD);
        }


        [HttpPost]
        public ActionResult AddCard(CardDetails objCD)
        {
            bool res = false;

            #region Bigger
            if (Request.Form["bigmembername"] != null && Request.Form["bigmembername"] != "")
            {
                objCD.BiggerName = Request.Form["bigmembername"].Split(',');

            }
            if (Request.Form["bigmemberage"] != null && Request.Form["bigmemberage"] != "")
            {
                objCD.BiggerAge = Request.Form["bigmemberage"].Split(',');

            }
            if (Request.Form["biggergender"] != null && Request.Form["biggergender"] != "")
            {
                objCD.BiggerGender = Request.Form["biggergender"].Split(',');

            }
            #endregion

            #region Smaller
            if (Request.Form["smallmembername"] != null && Request.Form["smallmembername"] != "")
            {
                objCD.SmallerName = Request.Form["smallmembername"].Split(',');

            }
            if (Request.Form["smallmemberage"] != null && Request.Form["smallmemberage"] != "")
            {
                objCD.SmallerAge = Request.Form["smallmemberage"].Split(',');
            }
            if (Request.Form["smallergender"] != null && Request.Form["smallergender"] != "")
            {
                objCD.SmallerGender = Request.Form["smallergender"].Split(',');

            }
            #endregion

            objCD.Frompage = Request.Form["Frompage"] != null & Request.Form["Frompage"] != "" ? Request.Form["Frompage"] : "AddCard";

            objCD = cardservice.CalculateUnitAllocation(objCD);

            string param = RenderViewToString(this.ControllerContext, "~/Views/Templates/InsertCardData.cshtml", objCD);

            res = carddatadao.InsetCardData(param);

            return RedirectToAction("AddCard", "Card");
        }
        #endregion

        #region Members
        [HttpPost]
        public ActionResult MembersAjax(string type = "", int Cnt = 0)
        {
            ViewBag.type = type;
            ViewBag.Cnt = Cnt;
            return View();
        }
        #endregion

        #region UnitWiseProduct
        public ActionResult UnitWiseProductAllotment()
        {
            IList<UnitWiseProductAllotment> UnitWiseProductAllotmentList = lookup.GetUnitWiseProductAllotmentData();

            ViewBag.UnitWiseProductAllotmentList = UnitWiseProductAllotmentList;

            return View();
        }
        public string UnitWiseProductAllotmentAjaxUpdate(string pk, string value, string name)
        {
            string res = "";
            bool result = false;
            Hashtable ht = new Hashtable();
            string ip = !string.IsNullOrEmpty(Request.ServerVariables["REMOTE_ADDR"].ToString()) ? Request.ServerVariables["REMOTE_ADDR"].ToString() : "";

            ht.Add("id", pk);
            ht.Add("value", value);
            ht.Add("field", name);
            ht.Add("userid", 1);
            ht.Add("ip", ip);

            result = carddatadao.UpdateUnitWiseProductAllotment(ht);

            return res;
        }
        #endregion

        #region ActivateCard

        public ActionResult ActivateCard()
        {
            IList<CardDetails> CardList = null;



            int CardId = (Request.Form["CardId"] != null && Request.Form["CardId"] != "") ? Convert.ToInt32(Request.Form["CardId"]) : (Request.QueryString["CardId"] != null && Request.QueryString["CardId"] != "") ? Convert.ToInt32(Request.QueryString["CardId"]) : 0;


            Hashtable ht = new Hashtable();

            ht.Add("Option", "all");
            ht.Add("CardId", CardId);

            string param = RenderViewToString(this.ControllerContext, "~/Views/Templates/GetCardData.cshtml", ht);

            CardList = carddatadao.GetCard(param);

            ViewBag.CardList = CardList;

            return View();
        }

        [HttpPost]
        public string ActivateDeactivateCardAjax()
        {

            string res = "";

            IList result = null;

            int CardId = (Request.Form["CardId"] != null && Request.Form["CardId"] != "") ? Convert.ToInt32(Request.Form["CardId"]) : (Request.QueryString["CardId"] != null && Request.QueryString["CardId"] != "") ? Convert.ToInt32(Request.QueryString["CardId"]) : 0;

            string Status = (Request.Form["Status"] != null && Request.Form["Status"] != "") ? Convert.ToString(Request.Form["Status"]) : (Request.QueryString["Status"] != null && Request.QueryString["Status"] != "") ? Convert.ToString(Request.QueryString["Status"]) : "0";

            Hashtable ht = new Hashtable();

            ht.Add("Option", "activatedeactivatecard");
            ht.Add("CardId", CardId);
            ht.Add("Status", Status);

            string param = RenderViewToString(this.ControllerContext, "~/Views/Templates/GetCardData.cshtml", ht);

            res = carddatadao.GetCard(param);


            return res;
        }

        #endregion
    }
}
