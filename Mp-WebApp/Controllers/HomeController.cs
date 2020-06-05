using Business_Logic.Models;
using Business_Logic.Processor;
using System.Web.Mvc;
using System.Web.Security;

namespace Mp_WebApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {

            var models = StoreItemProcessor.ConvertAllStoreItemToModels();

            return View(models);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

          
            return View();
        }

    }
}