using System.Collections.Generic;
using Business_Logic.Processor;
using Business_Logic.Models;
using System.Web.Mvc;
using System.Web;
using PagedList.Mvc;
using PagedList;
using System;
using System.Web.Security;
using System.Linq;

namespace Mp_WebApp.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ProductsController : Controller
    {
        [AllowAnonymous]
        public ActionResult AllProducts(string search,int? i)
        {
            var StoreItems = StoreItemProcessor.ConvertAllStoreItemToModels();


            return View(StoreItems
                .Where(x => search == null || x.Name.ToLower().StartsWith(search.ToLower()))
                .ToList()
                .ToPagedList(i ?? 1, 5));
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
                var imagemodels = StoreItemProcessor.ConvertToImageModel(imageData);

                model.Images = imagemodels;
            }
            model.Id = Guid.NewGuid();
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
            var checkModel = StoreItemProcessor.GetStoreItemModelbyId(model.Id);

            if (StoreImages != null && StoreImages[0] != null)
            {
                var imageData = StoreItemProcessor.ConverToBytes(StoreImages);
                var imagemodels = StoreItemProcessor.ConvertToImageModel(imageData);

                model.Images = imagemodels;
            }
            StoreItemProcessor.UpdateStoreItemImages(model);
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

        [AllowAnonymous]
        public ActionResult Details(Guid id)
        {
            var model = StoreItemProcessor.GetStoreItemModelbyId(id);

            if (model == null)
                return View();

            return View(model);
        }
    }
}