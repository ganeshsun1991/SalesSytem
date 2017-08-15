using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TNSalesSystem.Domain.Common;
using TNSalesSystem.Domain.Product_Details;
using TNSalesSystem.Persistence.Interfaces;
using TNSalesSystem.Persistence.Implementations;

namespace TNSalesSystem.Controllers.Product
{
    public class ProductController : Controller
    {
        //
        // GET: /Product/
        IProductDatadao productdao = new Productdatadao();
        ILookup lookup = new Lookup();

        #region Registerlookup
        public void ProductRegisertLookUp()
        {
            #region ProductTypeList
            List<SelectListItem> ProductTypeList = new List<SelectListItem>();
            ProductTypeList.Add(new SelectListItem { Text = "Select", Value = "" });
            ProductTypeList.Add(new SelectListItem { Text = "Boil", Value = "Boil" });
            ProductTypeList.Add(new SelectListItem { Text = "Raw", Value = "Raw" });
            ProductTypeList.Add(new SelectListItem { Text = "AAY", Value = "AAY" });
            ProductTypeList.Add(new SelectListItem { Text = "OAP", Value = "OAP" });
            ProductTypeList.Add(new SelectListItem { Text = "AP", Value = "AP" });
            #endregion

            #region UnitList
            List<SelectListItem> UnitList = new List<SelectListItem>();
            // UnitList.Add(new SelectListItem { Text = "Select", Value = "" });
            UnitList.Add(new SelectListItem { Text = "K.G", Value = "K.G" });
            UnitList.Add(new SelectListItem { Text = "Liter", Value = "Liter" });
            UnitList.Add(new SelectListItem { Text = "Packet", Value = "Packet(1 K.G)" });
            #endregion
            SelectList ProductTypes = new SelectList(ProductTypeList, "Text", "Value");
            SelectList Units = new SelectList(UnitList, "Text", "Value");

            ViewBag.ProductTypes = ProductTypes;
            ViewBag.Units = Units;
        }
        #endregion

        #region ProductInsert
        [HttpPost]
        public ActionResult ProductInsert(ProductDetails objPD)
        {
            bool res = false;
            objPD.ProductType = objPD.ProductType != null && objPD.ProductType == "Select" ? "" : objPD.ProductType;
            res = productdao.InsertProductData(objPD);
            return RedirectToAction("ProductInsert");
        }

        public ActionResult ProductInsert()
        {
            ProductRegisertLookUp();
            return View();
        }
        #endregion

        #region ProductAjax
        public ActionResult ProductDetailsAjax()
        {
            IList<ProductDetails> productdetailslist = null;
            productdetailslist = lookup.GetProductDetailsData();
            ViewBag.productdetailslist = productdetailslist;
            return View();
        }
        #endregion


    }
}
