using Business_Logic.Models;
using Business_Logic.Processor;
using System.Web.Security;
using System.Web.Mvc;
using System.Linq;
using PagedList;
using System;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Security.Cryptography.X509Certificates;

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

        [Authorize(Roles = "Admin,Customer")]
        public ActionResult UserInformation(Guid UserId)
        {
            var model = UserProcessor.GetUserToModel(UserId);

            ViewBag.Message = $"User: {model.Name}";
            return View(model);
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
            Guid userId = GetUserId();

            var userModel = UserProcessor.GetUserToModel(userId);

            if (userModel == null)
                return View();

            return View(userModel.ShoppingCart);
        }

        [Authorize(Roles = "Customer")]
        public ActionResult AddToShoppingCart(Guid id)
        {
            Guid userId = GetUserId();

            var storeItem = StoreItemProcessor.GetStoreItemModelbyId(id);
            ShoppingCartProcessor.AddToShoppingCart(userId, storeItem);

            return RedirectToAction("ShoppingCart", "Acount");
        }

        [Authorize(Roles = "Customer")]
        public ActionResult RemoveFromShoppingCart(Guid id)
        {
            Guid userId = GetUserId();

            var shoppingModel = ShoppingCartProcessor.GetShoppingModel(id);
            ShoppingCartProcessor.RemoveFromShoppingCart(userId, shoppingModel);

            return RedirectToAction("ShoppingCart", "Acount");
        }

        public ActionResult EditShoppingCart(Guid id)
        {
            Guid userId = GetUserId();
            var model = ShoppingCartProcessor.GetShoppingModel(id);
            var userModel = UserProcessor.GetUserToModel(userId);

            var checkUserOwnesItem = userModel.ShoppingCart.Where(x => x.Id == id).SingleOrDefault();

            if (model == null || checkUserOwnesItem == null)
            {
                return RedirectToAction("ShoppingCart", "Acount");
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult EditShoppingCart(ShoppingCartModel model)
        {
            Guid userId = GetUserId();
            var user = UserProcessor.GetUserToModel(userId);
            var userModel = UserProcessor.GetUserToModel(userId);

            var checkUserOwnesItem = userModel.ShoppingCart.Where(x => x.Id == model.Id).SingleOrDefault();
            var storeItem = StoreItemProcessor.GetStoreItemModelbyId(checkUserOwnesItem.StoreItemId);

            var StockAmountIsGreater = storeItem.InStock >= model.Amount;

            if (model == null || checkUserOwnesItem == null || !StockAmountIsGreater)
            {
                return RedirectToAction("EditShoppingCart", "Acount");
            }
            ShoppingCartProcessor.UpdateShoppingCart(checkUserOwnesItem,model);
            return RedirectToAction("ShoppingCart", "Acount");
        }
        public ActionResult SendPaymentEmail()
        {
            var userId = GetUserId();

            var userModel = UserProcessor.GetUserToModel(userId);
            var mailBody = ShoppingCartToString(userModel);

            if (!SendEmail(userId, "MP-Verf Order confirmation email", mailBody))
            {
                //email did not send do stuff

                return RedirectToAction("ShoppingCart", "Acount");
            }
            //clear item list 
            foreach (var storeItem in userModel.ShoppingCart)
            {
                ShoppingCartProcessor.RemoveFromShoppingCart(userId, storeItem);
            } 

            return RedirectToAction("ShoppingCart", "Acount");
        }

        private Guid GetUserId()
        {
            string identity = User.Identity.Name;
            Guid userId = Guid.Parse(identity);
            return userId;
        }
        public bool SendEmail(Guid userId ,string subject,string emailBody)
        {
            try
            {
                var userModel = UserProcessor.GetUserToModel(userId);

                string senderEmail = System.Configuration.ConfigurationManager.AppSettings["SenderEmail"].ToString();
                string senderPassword = System.Configuration.ConfigurationManager.AppSettings["SenderPassword"].ToString();

                SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
                // Set encrypted connection to true
                client.EnableSsl = true;
                client.Timeout = 100000;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(senderEmail,senderPassword);
                // Whrite email
                MailMessage mailMessage = new MailMessage(senderEmail, userModel.Email, subject, emailBody);
                mailMessage.IsBodyHtml = true;
                mailMessage.BodyEncoding = UTF8Encoding.UTF8;

                client.Send(mailMessage);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
        public string ShoppingCartToString(UserModel userModel)
        {
            string result = "";
            foreach (var storeItem in userModel.ShoppingCart)
            {
                result += $"Name: {storeItem.Name} Amount: {storeItem.Amount} Price: € {storeItem.PriceTimesAmount}. ";
            }
            result += $"Total price: € {userModel.ShoppingCart.Sum(x => x.PriceTimesAmount)}.";

            return result;
        }
    }
}