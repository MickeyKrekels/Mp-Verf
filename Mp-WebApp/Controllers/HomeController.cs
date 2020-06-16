using Business_Logic.Models;
using Business_Logic.Processor;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;

namespace Mp_WebApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {

            var models = StoreItemProcessor.ConvertAllStoreItemToModels();
            models = models.Where(x => x.Discount != 0).ToList();
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