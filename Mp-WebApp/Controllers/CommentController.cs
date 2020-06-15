using Business_Logic.Models;
using Business_Logic.Processor;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mp_WebApp.Controllers
{
    public class CommentController : Controller
    {
        // GET: Comment
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [Authorize(Roles = "Customer")]
        public ActionResult PostComment(Guid storeItemId, string text)
        {
            if (text == null || text == "")
                return RedirectToAction("Details", "Products", new { id = storeItemId });

            string identity = User.Identity.Name;
            Guid userId = Guid.Parse(identity);

            CommentProcessor.AddComment(userId, storeItemId, text);

            return RedirectToAction("Details", "Products", new { id = storeItemId });
        }

        [Authorize(Roles = "Admin,Customer")]
        public ActionResult RemoveComment(Guid Id)
        {
            string identity = User.Identity.Name;
            Guid userId = Guid.Parse(identity);

            var model = CommentProcessor.GetUserComment(Id);

            if(model == null)
                return RedirectToAction("AllProducts", "Products");

            if (userId != model.OwnerId)
                return RedirectToAction("Details","Products",new { id = model.StoreItemId });

            CommentProcessor.RemoveComment(Id);

            return RedirectToAction("Details", "Products", new { id = model.StoreItemId });
        }

        [Authorize(Roles = "Customer")]
        public ActionResult CommentRating(Guid commentId,Guid storeItemId)
        {

            CommentRatingModel model = new CommentRatingModel()
            {
                Id = Guid.NewGuid(),
                CommentId = commentId,     
                StoreItemId = storeItemId,
            };

            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Customer")]
        public ActionResult CommentRating(CommentRatingModel model)
        {
            string identity = User.Identity.Name;
            Guid userId = Guid.Parse(identity);

            if (model == null)
                return View(model);

            model.OwnerId = userId;
            model.Id = Guid.NewGuid();
            model.DataCreated = DateTime.Now;

            CommentProcessor.AddCommentRating(model);
            return RedirectToAction("Details", "Products", new { id = model.StoreItemId});
        }

        [Authorize(Roles = "Customer")]
        public ActionResult EditCommentRating(Guid commentRatingId)
        {
            var model = CommentProcessor.GetCommentRating(commentRatingId);

            return View(model);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult ComplaintsList(string search, int? i)
        {
            ViewBag.Message = "All Complaints";
            var models = CommentProcessor.GetCommentRating();

            return View(models
                .Where(x => search == null || x.OpinionText.ToLower().StartsWith(search.ToLower()))
                .Where(x => x.Report)
                .ToList()
                .ToPagedList(i ?? 1, 10));
        }

        [Authorize(Roles = "Admin")]
        public ActionResult CommentRatingDetails(Guid id)
        {
            var model = CommentProcessor.GetCommentRating(id);
            var reportedComment = CommentProcessor.GetUserComment(model.CommentId);
            ViewData["ReportedComment"] = reportedComment;

            return View(model);
        }

       [HttpPost]
        [Authorize(Roles = "Customer")]
        public ActionResult EditCommentRating(CommentRatingModel model)
        {
            CommentProcessor.UpdateCommentRating(model);
            return RedirectToAction("Details", "Products", new { id = model.StoreItemId });
        }

        [Authorize(Roles = "Admin,Customer")]
        public ActionResult RemoveCommentRating(Guid commentRatingId)
        {
            var model = CommentProcessor.GetCommentRating(commentRatingId);
            CommentProcessor.RemoveCommentRating(commentRatingId);
            return RedirectToAction("Details", "Products", new { id = model.StoreItemId });
        }

    }
}