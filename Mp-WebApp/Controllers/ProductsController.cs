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
    public class ProductsController : Controller
    {
        [AllowAnonymous]
        public ActionResult AllProducts(string search, string sortBy, int? i)
        {
            var StoreItems = StoreItemProcessor.ConvertAllStoreItemToModels()
                .Where(x => search == null || x.Name.ToLower()
                .StartsWith(search.ToLower()))
                .ToList();

            if (sortBy != "Sort By")
            {
                if (sortBy == "Price up")
                {
                    StoreItems = StoreItems.OrderByDescending(x => x.PriceWithDiscount).ToList();
                }
                if (sortBy == "Price down")
                {
                    StoreItems = StoreItems.OrderBy(x => x.PriceWithDiscount).ToList();
                }
                if (sortBy == "In Stock up")
                {
                    StoreItems = StoreItems.OrderByDescending(x => x.InStock).ToList();
                }
                if (sortBy == "In Stock down")
                {
                    StoreItems = StoreItems.OrderBy(x => x.InStock).ToList();
                }
            }

            return View(StoreItems.ToPagedList(i ?? 1, 8));
        }
        [Authorize(Roles = "Admin")]
        public ActionResult AddProduct()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(Guid id)
        {
            var model = StoreItemProcessor.GetStoreItemModelbyId(id);

            if (model == null)
                return View();

            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
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

        [Authorize(Roles = "Admin")]
        public ActionResult Delete(Guid id)
        {
            var model = StoreItemProcessor.GetStoreItemModelbyId(id);

            if (model == null)
                return View();

            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
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