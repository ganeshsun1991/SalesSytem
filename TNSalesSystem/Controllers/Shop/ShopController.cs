using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TNSalesSystem.Domain;
using TNSalesSystem.Domain.Common;
using TNSalesSystem.Domain.Shop;
using TNSalesSystem.Persistence;
using TNSalesSystem.Persistence.Implementations;
using TNSalesSystem.Persistence.Interfaces;

namespace TNSalesSystem.Controllers.Shop
{
    public class ShopController : Controller
    {
        //
        // GET: /Shop/
        ILookup lookup = new Lookup();
        IShopDatadao shopdatadao = new ShopDatadao();

        public ActionResult ShopDataNew()
        {

            CommonData dummy = new CommonData
            {
                Name = "-Select-",
                Id = 0
            };

            IList<CommonData> CountryList = lookup.GetDropDownData("Country");
            SelectList Coutries = new SelectList(CountryList, "Id", "Name");
            ViewBag.Coutries = Coutries;

            IList<CommonData> StateList = lookup.GetDropDownData("State");
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

            return View();
        }
        [HttpPost]
        public ActionResult ShopDataNew(ShopData objSD)
        {
            bool res = false;
            //res = shopdatadao.InsertShopData(objSD);
            return RedirectToAction("ShopDataNew", "Shop");
        }
        public string GetShopNameAjax(string city)
        {
            string res = "";
            res = lookup.GetShopName(city);
            return res;
        }
    }
}
