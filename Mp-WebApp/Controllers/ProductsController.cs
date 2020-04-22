using Business_Logic.Models;
using Business_Logic.Processor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mp_WebApp.Controllers
{
    public class ProductsController : Controller
    {
        public ActionResult AllProducts()
        {
            var StoreItems = StoreItemProcessor.ConvertAllStoreItemToModels();


            return View(StoreItems);
        }
        
        [Authorize(Roles = "Admin")]
        public ActionResult AddProduct()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult AddProduct(StoreItemModel model)
        {
            if (model == null || !ModelState.IsValid)
                return View();

            StoreItemProcessor.CreateStoreItem(model);

            return RedirectToAction("AllProducts");
        }
    }
}