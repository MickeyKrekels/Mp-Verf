﻿using Business_Logic.Models;
using Business_Logic.Processor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Mp_WebApp.Controllers
{
    public class AcountController : Controller
    {
        // GET: Acount
        //public ActionResult Index()
        //{
        //    return View();
        //}

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
                return RedirectToAction("Index");
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
        public ActionResult UserList()
        {
            ViewBag.Message = "All users";
            var models = UserProcessor.ConvertAllUsersToModel();
            return View(models);
        }
    }
}