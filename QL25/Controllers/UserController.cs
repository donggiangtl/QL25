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
    public class UserController : Controller
    {
        private MyDB db = new MyDB();
        // GET: User
        public ActionResult Index(int dV_ID)
        {
            TempData["dV_ID"] = dV_ID;
            ViewBag.UserName = TempData["UserName"];
            ViewBag.dV_ID = TempData["dV_ID"];
            ViewBag.TenDV = db.GetTenDV(dV_ID);
            return View();
        }

        public ActionResult CanBoNhanVienIndex(int? dV_ID, string isShow)
        {
            if (dV_ID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            int I_dv_ID = (int)dV_ID;
            TempData["dV_ID"] = I_dv_ID;
            ViewBag.dV_ID = TempData["dV_ID"];
            TempData["UserName"] = db.GetTenDV(I_dv_ID);
            ViewBag.UserName = TempData["UserName"];
            var root = db.GetDVSystem(I_dv_ID);
            ViewBag.job_level = new SelectList(db.CapChuyenMons, "ID", "TenCap");
            ViewBag.study_level = new SelectList(db.BacDaoTaos, "ID", "TenBac");
            ViewBag.military_rank = new SelectList(db.QHs, "ID", "CapHam");
            ViewBag.child = root.Children.Count;
           
            return View(root);
        }

        public ActionResult CanBoNhanVienList(int? dV_ID)
        {
            if (!dV_ID.HasValue)
                return PartialView("CanBoNhanVienList", new List<HC_ShowModel>());
            var model = db.GetHC_ShowModelsByDV((int)dV_ID);
            return PartialView("CanBoNhanVienList", model);
        }
        public ActionResult CanBoNhanVienListAll(int dV_ID)
        {
            var model = db.GetHC_ShowModelsByDVAll(dV_ID);
            return PartialView("CanBoNhanVienList", model);
        }

        public ActionResult GetHCsforDataTable(int dV_ID)
        {
            var model = db.GetHCsByDV(dV_ID);
            return Json(new { data = model }, JsonRequestBehavior.AllowGet);
        }
        
        public string GetListTitle(int? dV_ID)
        {
            if (dV_ID == null)
                return "ID đơn vị = null";
            else
                return db.DVs.Find(dV_ID).TenDV;
        }

        public ActionResult Respone(string dV_ID)
        {
            ViewBag.Msg = TempData["Message"];
            ViewBag.dV_ID = dV_ID;
            return View();
        }
       //[CustomAuthorize("User")]
        [HttpPost]
        public ActionResult CreateHCByContextMenu(string BC_ID, string HC_Name, DateTime date_of_birth, DateTime start_date,  int job_level, int study_level, int military_rank, string dV_ID)
        {
            int iBC_ID = int.Parse(BC_ID.Split('*')[1]);
            if (db.AddNewHC(iBC_ID, HC_Name, date_of_birth, start_date, null, job_level, study_level, military_rank))
                return RedirectToAction("CanBoNhanVienIndex", new { dV_ID });

            TempData["Message"] = "HC_Name = " + HC_Name + "; date_of_birth = " + date_of_birth.ToShortDateString();
            TempData["dV_ID"] = dV_ID;
            return RedirectToAction("Respone", new { dV_ID });
        }


        /// <summary>
        /// Nhớ trạng thái của TreeNode vào cơ sở dữ liệu
        /// </summary>
        /// <param name="id">id của TreeNode</param>
        /// <param name="isOpen">Trạng thái của TreeNode</param>
        /// <returns></returns>
        [CustomAuthorize("User")]
        [HttpPost]
        public ActionResult SaveNodeState(int id, bool isOpen)
        {
            db.UpdateDVTreeNodeState(id, isOpen);
            return Json(new { success = true });
        }

        [CustomAuthorize("User")]
        public ActionResult HCEdit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HC hC1 = db.HCs.Find(id);
            if (hC1 == null)
            {
                return HttpNotFound();
            }
            HC_ShowModel hC = db.HC2HC_ShowMode(hC1);
            hC.intParam_ID = int.Parse(Session["dV_ID"].ToString());
            ViewBag.BacDaoTao_ID = new SelectList(db.BacDaoTaos, "ID", "TenBac", hC.BacDaoTao_ID);
            ViewBag.BC_ID = new SelectList(db.BCs, "ID", "ID", hC.BC_ID);
            ViewBag.CapChuyenMon_ID = new SelectList(db.CapChuyenMons, "ID", "TenCap", hC.CapChuyenMon_ID);
            ViewBag.QH_ID = new SelectList(db.QHs, "ID", "CapHam", hC.QH_ID);
            ViewBag.Msg = "";
            return View(hC);
        }

        [CustomAuthorize("User")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult HCEdit(HC_ShowModel hC_ShowModel)
        {
            if (ModelState.IsValid)
            {
                HC hC = db.HCs.Find(hC_ShowModel.ID);
                hC.UpdateFromModel(hC_ShowModel);
                db.Entry(hC).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("CanBoNhanVienIndex", new { dV_ID = hC_ShowModel.intParam_ID});
            }
            ViewBag.BacDaoTao_ID = new SelectList(db.BacDaoTaos, "ID", "TenBac", hC_ShowModel.BacDaoTao_ID);
            ViewBag.BC_ID = new SelectList(db.BCs, "ID", "ID", hC_ShowModel.BC_ID);
            ViewBag.CapChuyenMon_ID = new SelectList(db.CapChuyenMons, "ID", "TenCap", hC_ShowModel.CapChuyenMon_ID);
            ViewBag.QH_ID = new SelectList(db.QHs, "ID", "CapHam", hC_ShowModel.QH_ID);
            ViewBag.Msg = "Khong cap nhat duoc";
            return View(hC_ShowModel);
        }
    }
}