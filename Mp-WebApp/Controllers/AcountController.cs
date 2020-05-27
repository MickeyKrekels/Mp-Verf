﻿using Business_Logic.Models;
using Business_Logic.Processor;
using System.Web.Security;
using System.Web.Mvc;
using System.Linq;
using PagedList;
using System;
using System.Net.Mail;
using System.Net;
using System.Text;

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

            var userModel = UserProcessor.ConvertUserToModel(id);

            if (userModel == null)
                return View();

            return View(userModel.ShoppingCart);
        }

        [Authorize(Roles = "Customer")]
        public ActionResult AddToShoppingCart(Guid id)
        {
            string identity = User.Identity.Name;
            Guid userId = Guid.Parse(identity);

            var storeItem = StoreItemProcessor.GetStoreItemModelbyId(id);
            ShoppingCartProcessor.AddToShoppingCart(userId, storeItem);

            return RedirectToAction("ShoppingCart", "Acount");
        }

        [Authorize(Roles = "Customer")]
        public ActionResult RemoveFromShoppingCart(Guid id)
        {
            string identity = User.Identity.Name;
            Guid userId = Guid.Parse(identity);

            var storeItem = StoreItemProcessor.GetStoreItemModelbyId(id);
            ShoppingCartProcessor.RemoveFromShoppingCart(userId, storeItem);

            return RedirectToAction("ShoppingCart", "Acount");
        }
        public ActionResult SendPaymentEmail()
        {
            string identity = User.Identity.Name;
            Guid userId = Guid.Parse(identity);

            var userModel = UserProcessor.ConvertUserToModel(userId);

            if (!SendEmail(userId, "MP-Verf Order confirmation email", "test"))
            {
                //email did not send do stuff
                
                //return
            }
            //clear item list 
            foreach (var storeItem in userModel.ShoppingCart)
            {
                ShoppingCartProcessor.RemoveFromShoppingCart(userId, storeItem);
            } 

            return RedirectToAction("ShoppingCart", "Acount");
        }
        public bool SendEmail(Guid userId ,string subject,string emailBody)
        {
            try
            {

                var userModel = UserProcessor.ConvertUserToModel(userId);

                string senderEmail = System.Configuration.ConfigurationManager.AppSettings["SenderEmail"].ToString();
                string senderPassword = System.Configuration.ConfigurationManager.AppSettings["SenderPassword"].ToString();

                SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
                // Set encrypted connection to true
                client.EnableSsl = true;
                client.Timeout = 100000;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(senderEmail, senderPassword);
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

    }
}