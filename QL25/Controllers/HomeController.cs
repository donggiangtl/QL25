using DataIO.MyModel;
using DataIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;

namespace QL25.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        [CustomAuthorize("User")]
        public ActionResult Index(int? dv_ID)
        {

            ViewBag.UserName = "";
            return View();
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