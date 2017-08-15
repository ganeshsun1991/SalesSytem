using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TNSalesSystem.Domain.Common;
using TNSalesSystem.Domain.User;
using TNSalesSystem.Persistence.Implementations;
using TNSalesSystem.Persistence.Interfaces;

namespace TNSalesSystem.Controllers.User
{
    public class UserController : Controller
    {
        //
        // GET: /User/
        ILookup lookup = new Lookup();
        IUserDatadao user = new UserDatadao();


        public ActionResult AddUser()
        {
            CommonData dummy = new CommonData
            {
                Name = "-Select-",
                Id = 0
            };

            IList<CommonData> CountryList = lookup.GetDropDownData("Country");
            CountryList.Insert(0, dummy);
            SelectList Coutries = new SelectList(CountryList, "Id", "Name");
            ViewBag.Coutries = Coutries;

            IList<CommonData> StateList = lookup.GetDropDownData("State");
            StateList.Insert(0, dummy);
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

            IList<CommonData> RoleList = lookup.GetDropDownData("Role");
            //RoleList.Insert(0, dummy);
            SelectList Roles = new SelectList(RoleList, "Id", "Name");
            ViewBag.Roles = Roles;
            return View();
        }

        [HttpPost]
        public ActionResult AddUser(UserData objUD)
        {
            bool res = false;
            res=user.InsertUserData(objUD);
            return RedirectToAction("AddUser", "User");

        }

        public ActionResult EditUser(int page = 1)
        {
            var pageSize = 5;
            var totalItems = 0;
            var startPage = page;
            var endPage = page + 4;
            IList<UserData> UserList = user.GetUserList("list", startPage, endPage, out totalItems);


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
            return View();
        }

        public string CheckLoginnameAvaliablityAjax(string Loginname)
        {
            string result = "";
            bool res = false;
            res = user.CheckLoginnameAvaliablity(Loginname);
            result = res ? "notok" : "ok";
            return result;
        }

        [HttpGet]
        public ActionResult GetUserListAjax()
        {
            string res = "";
            List<UserData> objLI = new List<UserData>();
            objLI = user.GetUserList();
            if (objLI != null && objLI.Count > 0)
            {
                res = JsonConvert.SerializeObject(objLI);
            }
            return Json(res,JsonRequestBehavior.AllowGet);
        }
    }
}
