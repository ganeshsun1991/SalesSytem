using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Collections;
using TNSalesSystem.Domain.Common;
using TNSalesSystem.Persistence.Interfaces;
using TNSalesSystem.Persistence.Implementations;

namespace TNSalesSystem.Controllers.Common
{
    public class CommonController : Controller
    {
        //
        // GET: /Common/
        ILookup lookup = new Lookup();
        ICommonDatadao commondatadao = new CommonDatadao();

        //public CommonController(ILookup _lookup)
        //{
        //    this.lookup = _lookup;
        //}

        public ActionResult CommonData()
        {
            return View();
        }

        public ActionResult CommonDataAjax(int page = 1)
        {
            Hashtable ht = new Hashtable();
            IList<CommonData> commondatalist = null;


            var pageSize = 5;
            var totalItems = 0;
            var startPage = (page - 1) * pageSize == 0 ? 1 : (((page - 1) * pageSize) + 1);
            var endPage = page * pageSize;

            #region Search Cretiria

            if (Request.Form["txtname"] != null && Request.Form["txtname"] != "")
                ht["name"] = Request.Form["txtname"];
            else
                ht["name"] = "";

            if (Request.Form["txtvalue"] != null && Request.Form["txtvalue"] != "")

                ht["value"] = Request.Form["txtvalue"];
            else
                ht["value"] = "";

            if (Request.Form["txtdate"] != null && Request.Form["txtdate"] != "")
                ht["date"] = Request.Form["txtdate"];
            else
                ht["date"] = "";

            if (Request.Form["drpcommontype"] != null && Request.Form["drpcommontype"] != "")
                ht["type"] = Request.Form["drpcommontype"];
            else
                ht["type"] = "";

            ht["startrow"] = startPage;
            ht["endrow"] = endPage;

            #endregion



            commondatalist = lookup.GetCommonData(ht);

            totalItems = commondatalist != null && commondatalist.Count >= 0 ? commondatalist[0].TotalCount : 0;

            var totalPages = (int)Math.Ceiling((decimal)totalItems / (decimal)pageSize);
            var currentPage = page != null ? (int)page : 1;

            if (startPage <= 0)
            {
                endPage -= (startPage - 1);
                startPage = 1;
            }
            if (endPage > totalPages)
            {
                endPage = totalPages;
                if (endPage > 10)
                {
                    startPage = endPage - 9;
                }
            }

            ViewBag.TotalItems = totalItems;
            ViewBag.CurrentPage = currentPage;
            ViewBag.PageSize = pageSize;
            ViewBag.TotalPages = totalPages;
            ViewBag.StartPage = startPage;
            ViewBag.EndPage = endPage;
            ViewBag.commondatalist = commondatalist;

            return View();
        }
        [HttpPost]
        public string AddCommonData()
        {
            CommonData cd = new CommonData();
            bool res = false;
            string mes = "";
            if (Request.Form["drpcommontypedialog"] != null && Request.Form["drpcommontypedialog"] != "")
                cd.Type = Request.Form["drpcommontypedialog"];
            else
                cd.Type = "";
            if (Request.Form["txtnamedialog"] != null && Request.Form["txtnamedialog"] != "")
                cd.Name = Request.Form["txtnamedialog"];
            else
                cd.Name = "";
            if (Request.Form["txtvaluedialog"] != null && Request.Form["txtvaluedialog"] != "")
                cd.Value = Request.Form["txtvaluedialog"];
            else
                cd.Value = "";
            if (Request.Form["txtdescrptiondialog"] != null && Request.Form["txtdescrptiondialog"] != "")
                cd.Descrption = Request.Form["txtdescrptiondialog"];
            else
                cd.Descrption = "";
            res = commondatadao.InsertCommonData(cd);
            if (res == true)
            {
                mes = "Common data saved successfuy.";
            }
            return mes;
        }
        public string CommonDataDeleteAjax(int id = 0, string mode = "")
        {
            bool res = false;
            string mes = "";
            string ip = !string.IsNullOrEmpty(Request.ServerVariables["REMOTE_ADDR"].ToString()) ? Request.ServerVariables["REMOTE_ADDR"].ToString() : "";

            Hashtable ht = new Hashtable();
            ht.Add("mode", mode);
            ht.Add("user", "admin");
            ht.Add("id", id);
            ht.Add("ip", ip);
            ht.Add("field", "IsDeleted");
            ht.Add("value", "1");
            res = commondatadao.DeleteCommonData(ht);

            if (res == true)
            {
                mes = "ok";
            }
            return mes;
        }
        public string CommonDataUpdataAjax(string pk, string value, string name)
        {
            bool res = false;
            string mes = "";
            string ip = !string.IsNullOrEmpty(Request.ServerVariables["REMOTE_ADDR"].ToString()) ? Request.ServerVariables["REMOTE_ADDR"].ToString() : "";
            Hashtable ht = new Hashtable();
            ht.Add("mode", "update");
            ht.Add("user", "admin");
            ht.Add("id", pk);
            ht.Add("ip", ip);
            ht.Add("field", name);
            ht.Add("value", value);

            res = commondatadao.UpdataCommonData(ht);

            if (res == true)
            {
                mes = "ok";
            }
            return mes;
        }
    }
}
