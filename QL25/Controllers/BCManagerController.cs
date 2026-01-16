using DataIO;
using DataIO.Data;
using DataIO.MyModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace QL25.Controllers
{
    public class BCManagerController : Controller
    {
        private MyDB db = new MyDB();

        public ActionResult Respone()
        {
            return View();
        }

        //id in Tree, {lv}x{dv_id}
        public ActionResult Tree2Show(string id)
        {            
            int lv, dv_id;
            if (id.Contains("d"))
            {
                string[] strings = id.Split('d');
                lv = int.Parse(strings[0]);
                dv_id = int.Parse(strings[1]);
                return RedirectToAction("Index", new { lv = lv, dv_id = dv_id });
            }
            ViewBag.ResponeMsg = "Thông tin sai lệch";
            return RedirectToAction("Respone");
        }
        // GET: BCManager
        
        
       


        
    }
}