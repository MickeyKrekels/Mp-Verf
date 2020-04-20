using Mp_WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mp_WebApp.Controllers
{
    public class PaintController : Controller
    {
        // GET: Paint
        [Route("Paint")]
        public ActionResult AllPaint()
        {
            return Content("list of all paint..");
        }




    }
}