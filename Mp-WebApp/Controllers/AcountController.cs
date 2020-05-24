using Business_Logic.Models;
using Business_Logic.Processor;
using System.Web.Security;
using System.Web.Mvc;
using System.Linq;
using PagedList;
using System;

namespace Mp_WebApp.Controllers
{
    public class AcountController : Controller
    {
        public ActionResult SignUp()
        {
            ViewBag.Message = "Please Sign up";
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SignUp(UserModel model)
        {

            if (ModelState.IsValid)
            {
                UserProcessor.CreateCustomer(model);
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Login()
        {
            ViewBag.Message = "Please Login";
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(UserModel model)
        {
            ViewBag.Message = "Please Login";

            var user = UserProcessor.UserLogin(model);

            if (user == null)
                return View();

            FormsAuthentication.SetAuthCookie(user.Id.ToString(), false);
            return RedirectToAction("Index", "Home");

        }

        [Authorize(Roles = "Admin")]
        public ActionResult UserList(string search, int? i)
        {
            ViewBag.Message = "All users";
            var models = UserProcessor.ConvertAllUsersToModel();
            return View(models
                .Where(x => search == null || x.Name.ToLower().StartsWith(search.ToLower()))
                .ToList()
                .ToPagedList(i ?? 1, 10));
        }

        [Authorize(Roles = "Customer")]
        public ActionResult ShoppingCart()
        {
            string identity = User.Identity.Name;
            Guid id = Guid.Parse(identity);

            var result = UserProcessor.ConvertUserToModel(id);

            if(result == null)
                return View();

            return View(result.ShoppingCart);
        }

        [Authorize(Roles = "Customer")]
        public ActionResult AddToShoppingCart(Guid id)
        {
            string identity = User.Identity.Name;
            Guid userId = Guid.Parse(identity);

            var storeItem = StoreItemProcessor.GetStoreItemModelbyId(id);
            UserProcessor.AddToShoppingCart(userId, storeItem);

            return RedirectToAction("ShoppingCart","Acount");
        }

        [Authorize(Roles = "Customer")]
        public ActionResult RemoveFromShoppingCart(Guid id)
        {
            string identity = User.Identity.Name;
            Guid userId = Guid.Parse(identity);

            var storeItem = StoreItemProcessor.GetStoreItemModelbyId(id);
            UserProcessor.RemoveFromShoppingCart(userId, storeItem);

            return RedirectToAction("ShoppingCart", "Acount");
        }

    }
}