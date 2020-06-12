using Business_Logic.Models;
using Business_Logic.Processor;
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

        [Authorize(Roles = "Customer")]
        public ActionResult UpdateCommentRating(Guid storeItemId, Guid commentId)
        {
            var model = CommentProcessor.GetUserComment(commentId);

            string identity = User.Identity.Name;
            Guid userId = Guid.Parse(identity);

            if (model == null || userId != model.OwnerId)
                return RedirectToAction("Details", "Products", new { id = storeItemId });

            CommentProcessor.UpdateRating(commentId);

            return RedirectToAction("Details", "Products", new { id = storeItemId });
        }

        [Authorize(Roles = "Customer")]
        public ActionResult RemoveComment(CommentModel model)
        {
            string identity = User.Identity.Name;
            Guid userId = Guid.Parse(identity);

            if (model == null || userId != model.OwnerId)
                return RedirectToAction("Details","Products");

            CommentProcessor.RemoveComment(model.Id);

            return RedirectToAction("Details", "Products");
        }
    }
}