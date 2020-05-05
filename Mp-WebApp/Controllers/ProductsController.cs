using Business_Logic.Models;
using Business_Logic.Processor;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;

namespace Mp_WebApp.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ProductsController : Controller
    {
        [AllowAnonymous]
        public ActionResult AllProducts()
        {
            var StoreItems = StoreItemProcessor.ConvertAllStoreItemToModels();


            return View(StoreItems);
        }

        public ActionResult AddProduct()
        {
            return View();
        }
          
        [HttpPost]
        public ActionResult AddProduct(StoreItemModel model, List<HttpPostedFileBase> StoreImages)
        {
            if (model == null || !ModelState.IsValid)
                return View();

            if (StoreImages != null)
            {
                var imageData = StoreItemProcessor.ConverToBytes(StoreImages);
                model.Images = imageData;
            }

            StoreItemProcessor.CreateStoreItem(model);

            return RedirectToAction("AllProducts");
        }

        public ActionResult Edit(Guid id)
        {
            var model = StoreItemProcessor.GetStoreItemModelbyId(id);

            if (model == null)
                return View();

            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(StoreItemModel model, List<HttpPostedFileBase> StoreImages)
        {
            if (StoreImages != null)
            {
                var imageData = StoreItemProcessor.ConverToBytes(StoreImages);
                model.Images = imageData;
            }
            StoreItemProcessor.EditStoreItem(model);
            return RedirectToAction("AllProducts");
        }


        public ActionResult Delete(Guid id)
        {
            var model = StoreItemProcessor.GetStoreItemModelbyId(id);

            if (model == null)
                return View();

            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(StoreItemModel model)
        {
            StoreItemProcessor.RemoveStoreItem(model);
            return RedirectToAction("AllProducts");
        }

        public ActionResult Details(Guid id)
        {
            var model = StoreItemProcessor.GetStoreItemModelbyId(id);

            if (model == null)
                return View();

            return View(model);
        }
    }
}