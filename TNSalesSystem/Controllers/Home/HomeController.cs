using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TNSalesSystem.Persistence.Implementations;
using TNSalesSystem.Persistence.Interfaces;
using TNSalesSystem.Service.Implementations;
using TNSalesSystem.Service.Interfaces;

namespace TNSalesSystem.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        public HomeController()
        {
            //if (session["leftmenulist"] == null)
            //{
            //}
        }

        ILookup lookup = new Lookup();
        ICardDatadao carddatadao = new CardDatadao();
        ICardService cardservice = new CardService();

        public ActionResult Home()
        {
            //Session["LeftMenuList"] = lookup.GetMenuList(0, "main");

            ViewBag.FromPage = "Home";
            return View();
        }

    }
}
