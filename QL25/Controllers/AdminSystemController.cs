using DataIO;
using DataIO.Data;
using DataIO.MyModel;
using DataIO.MyModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace QL25.Controllers
{
    public class AdminSystemController : Controller
    {
        private MyDB db = new MyDB();
        // GET: AdminSystem
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Respone()
        {
            ViewBag.ResponeMsg = TempData["Message"];
           
            return PartialView("Respone");
        }

        #region CD_TuongDuong
        // GET: CD_TuongDuong
        [CustomAuthorize("Admin")]
        public ActionResult CD_TuongDuongIndex()
        {
            return View(db.CD_TuongDuong.ToList());
        }

        // GET: CD_TuongDuong/Create
        [CustomAuthorize("Admin")]
        public ActionResult CD_TuongDuongCreate()
        {
            CD_TuongDuong cD_TuongDuong = new CD_TuongDuong { ID = Helper.GetNewChucDanhTuongDuongID(db), TenChucDanhTD = "NoName"};
            return View( cD_TuongDuong);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize("Admin")]
        public ActionResult CD_TuongDuongCreate(CD_TuongDuong cD_TuongDuong)
        {
            if (ModelState.IsValid)
            {
                //cD_TuongDuong.ID = Helper.GetNewChucDanhTuongDuongID(db);
                db.CD_TuongDuong.Add(cD_TuongDuong);
                db.SaveChanges();
                return RedirectToAction("CD_TuongDuongIndex");
            }

            return View(cD_TuongDuong);
        }
        [CustomAuthorize("Admin")]
        public ActionResult CD_TuongDuongEdit(int id)
        {
            CD_TuongDuong cD_TuongDuong = db.CD_TuongDuong.Find(id);
            if (cD_TuongDuong == null)
            {
                return HttpNotFound();
            }
            return View(cD_TuongDuong);
        }
        [CustomAuthorize("Admin")]
        [HttpPost]
        public ActionResult CD_TuongDuongEdit(CD_TuongDuong model)
        {
            db.Entry(model).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("CD_TuongDuongIndex");
        }
        [CustomAuthorize("Admin")]
        public ActionResult CD_TuongDuongDelete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CD_TuongDuong cD_TuongDuong = db.CD_TuongDuong.Find(id);
            if (cD_TuongDuong == null)
            {
                return HttpNotFound();
            }
            return View(cD_TuongDuong);
        }
        // POST: CD_TuongDuong/Delete/5
        [HttpPost, ActionName("CD_TuongDuongDelete")]
        [ValidateAntiForgeryToken]
        [CustomAuthorize("Admin")]
        public ActionResult DeleteCDTDConfirmed(int id)
        {
            CD_TuongDuong cD_TuongDuong = db.CD_TuongDuong.Find(id);
            db.CD_TuongDuong.Remove(cD_TuongDuong);
            db.SaveChanges();
            return RedirectToAction("CD_TuongDuongIndex");
        }
        #endregion

        #region DV_TD
        // GET: 
        [CustomAuthorize("Admin")]
        public ActionResult DV_TDIndex()
        {
            return View(db.DV_TuongDuong.ToList());
        }
        // GET: Create
        [CustomAuthorize("Admin")]
        public ActionResult DV_TDCreate()
        {
            DV_TuongDuong model = new DV_TuongDuong { ID = Helper.GetNewDonViTuongDuongID(db), TenKieuDV = "NoName" };
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize("Admin")]
        public ActionResult DV_TDCreate(DV_TuongDuong model)
        {
            if (ModelState.IsValid)
            {
                //cD_TuongDuong.ID = Helper.GetNewChucDanhTuongDuongID(db);
                db.DV_TuongDuong.Add(model);
                db.SaveChanges();
                return RedirectToAction("DV_TDIndex");
            }
            return View(model);
        }
        [CustomAuthorize("Admin")]
        public ActionResult DV_TDEdit(int id)
        {
            DV_TuongDuong model = db.DV_TuongDuong.Find(id);
            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }
        [CustomAuthorize("Admin")]
        [HttpPost]
        public ActionResult DV_TDEdit(DV_TuongDuong model)
        {
            db.Entry(model).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("DV_TDIndex");
        }
        [CustomAuthorize("Admin")]
        public ActionResult DV_TDDelete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DV_TuongDuong model = db.DV_TuongDuong.Find(id);
            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }
        // POST: CD_TuongDuong/Delete/5
        [HttpPost, ActionName("DV_TDDelete")]
        [ValidateAntiForgeryToken]
        [CustomAuthorize("Admin")]
        public ActionResult DeleteDVConfirmed(int id)
        {
            DV_TuongDuong model = db.DV_TuongDuong.Find(id);
            db.DV_TuongDuong.Remove(model);
            db.SaveChanges();
            return RedirectToAction("DV_TDIndex");
        }

        #endregion

        #region Nganh
        [CustomAuthorize("Admin")]
        public ActionResult NganhIndex()
        {
            return View(db.Nganhs.ToList());
        }
       
        [CustomAuthorize("Admin")]
        public ActionResult NganhCreate()
        {
            Nganh model = new Nganh { ID = Helper.GetNewNganhID(db), TenNganh = "NoName" };
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize("Admin")]
        public ActionResult NganhCreate(Nganh model)
        {
            if (ModelState.IsValid)
            {
                db.Nganhs.Add(model);
                db.SaveChanges();
                return RedirectToAction("NganhIndex");
            }
            return View(model);
        }
        [CustomAuthorize("Admin")]
        public ActionResult NganhEdit(int id)
        {
            Nganh model = db.Nganhs.Find(id);
            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }
        [CustomAuthorize("Admin")]
        [HttpPost]
        public ActionResult NganhEdit(Nganh model)
        {
            db.Entry(model).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("NganhIndex");
        }
        [CustomAuthorize("Admin")]
        public ActionResult NganhDelete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Nganh model = db.Nganhs.Find(id);
            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }
        // POST: CD_TuongDuong/Delete/5
        [HttpPost, ActionName("NganhDelete")]
        [ValidateAntiForgeryToken]
        [CustomAuthorize("Admin")]
        public ActionResult DeleteNganhConfirmed(int id)
        {
            Nganh model = db.Nganhs.Find(id);
            db.Nganhs.Remove(model);
            db.SaveChanges();
            return RedirectToAction("NganhIndex");
        }


        #endregion

        #region ChuyenNganh
        [CustomAuthorize("Admin")]
        public ActionResult ChuyenNganhIndex()
        {
            return View(db.ChuyenNganhs.ToList());
        }

        [CustomAuthorize("Admin")]
        public ActionResult ChuyenNganhCreate()
        {
            ChuyenNganh model = new ChuyenNganh { ID = Helper.GetNewChuyenNganhID(db), TenChuyenNganh = "NoName" };
            ViewBag.NganhID = new SelectList(db.Nganhs, "ID", "TenNganh");
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize("Admin")]
        public ActionResult ChuyenNganhCreate(ChuyenNganh model)
        {
            if (ModelState.IsValid)
            {
                db.ChuyenNganhs.Add(model);
                db.SaveChanges();
                return RedirectToAction("ChuyenNganhIndex");
            }
            return View(model);
        }
        [CustomAuthorize("Admin")]
        public ActionResult ChuyenNganhEdit(int id)
        {
            ChuyenNganh model = db.ChuyenNganhs.Find(id);
            if (model == null)
            {
                return HttpNotFound();
            }
            ViewBag.NganhID = new SelectList(db.Nganhs, "ID", "TenNganh", model.NganhID);
            return View(model);
        }
        [CustomAuthorize("Admin")]
        [HttpPost]
        public ActionResult ChuyenNganhEdit(ChuyenNganh model)
        {
            db.Entry(model).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("ChuyenNganhIndex");
        }
        [CustomAuthorize("Admin")]
        public ActionResult ChuyenNganhDelete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChuyenNganh model = db.ChuyenNganhs.Find(id);
            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }
        // POST: CD_TuongDuong/Delete/5
        [HttpPost, ActionName("ChuyenNganhDelete")]
        [ValidateAntiForgeryToken]
        [CustomAuthorize("Admin")]
        public ActionResult DeleteChuyenNganhConfirmed(int id)
        {
            ChuyenNganh model = db.ChuyenNganhs.Find(id);
            db.ChuyenNganhs.Remove(model);
            db.SaveChanges();
            return RedirectToAction("ChuyenNganhIndex");
        }


        #endregion

        #region CD
        [CustomAuthorize("Admin")]
        public ActionResult CDIndex()
        {
            return View(db.CDs.ToList());
        }

        [CustomAuthorize("Admin")]
        public ActionResult CDCreate()
        {
            ViewBag.CD_TD_ID = new SelectList(db.CD_TuongDuong, "ID", "TenChucDanhTD");
            ViewBag.ChuyenNganhID = new SelectList(db.ChuyenNganhs, "ID", "TenChuyenNganh");
            
            CD model = new CD { ID = Helper.GetNewChucDanhID(db), TenCD = "NoName" };            
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize("Admin")]
        public ActionResult CDCreate(CD model)
        {
            if (ModelState.IsValid)
            {
                db.CDs.Add(model);
                db.SaveChanges();
                return RedirectToAction("CDIndex");
            }
            ViewBag.CD_TD_ID = new SelectList(db.CD_TuongDuong, "ID", "TenChucDanhTD", model.CD_TD_ID);
            ViewBag.ChuyenNganhID = new SelectList(db.ChuyenNganhs, "ID", "TenChuyenNganh", model.ChuyenNganhID);
            return View(model);
        }
        [CustomAuthorize("Admin")]
        public ActionResult CDEdit(int id)
        {
            CD model = db.CDs.Find(id);
            if (model == null)
            {
                return HttpNotFound();
            }
            ViewBag.CD_TD_ID = new SelectList(db.CD_TuongDuong, "ID", "TenChucDanhTD", model.CD_TD_ID);
            ViewBag.ChuyenNganhID = new SelectList(db.ChuyenNganhs, "ID", "TenChuyenNganh", model.ChuyenNganhID);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize("Admin")]
        public ActionResult CDEdit(CD model)
        {
            if (ModelState.IsValid)
            {
                db.Entry(model).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("CDIndex");
            }
            ViewBag.CD_TD_ID = new SelectList(db.CD_TuongDuong, "ID", "TenChucDanhTD", model.CD_TD_ID);
            ViewBag.ChuyenNganhID = new SelectList(db.ChuyenNganhs, "ID", "TenChuyenNganh", model.ChuyenNganhID);
            return View(model);



        }
        [CustomAuthorize("Admin")]
        public ActionResult CDDelete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CD model = db.CDs.Find(id);
            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        [HttpPost, ActionName("CDDelete")]
        [ValidateAntiForgeryToken]
        [CustomAuthorize("Admin")]
        public ActionResult DeleteCDConfirmed(int id)
        {
            CD model = db.CDs.Find(id);
            db.CDs.Remove(model);
            db.SaveChanges();
            return RedirectToAction("CDIndex");
        }


        #endregion

        #region System
        [CustomAuthorize("Admin")]
        public ActionResult System()
        {
            var root = db.GetDVSystem(0);
            ViewBag.DV_TD_ID = new SelectList(db.DV_TuongDuong, "ID", "TenKieuDV");
            ViewBag.DV_TD_ID_E = new SelectList(db.DV_TuongDuong, "ID", "TenKieuDV");
            ViewBag.CD_ID = new SelectList(db.CDs, "ID", "TenCD");
            ViewBag.ResponeMsg = TempData["Message"];           
            return View(root);
        }

        /// <summary>
        /// Nhớ trạng thái của TreeNode vào cơ sở dữ liệu
        /// </summary>
        /// <param name="id">id của TreeNode</param>
        /// <param name="isOpen">Trạng thái của TreeNode</param>
        /// <returns></returns>
        [CustomAuthorize("Admin")]
        [HttpPost]
        public ActionResult SaveNodeState(int id, bool isOpen)
        {
            db.UpdateDVTreeNodeState(id, isOpen);
            return Json(new { success = true });
        }
        [CustomAuthorize("Admin")]
        public ActionResult CreateDVByContextMenu()
        {
            ViewBag.Info = TempData["Message"];
            return PartialView("CreateDVByContextMenu");
        }
        [CustomAuthorize("Admin")]
        [HttpPost]
        public ActionResult CreateDVByContextMenu(string DV_CreateName, string DV_CreateSign, int? parentId, int? DV_TD_ID)
        {
            TempData["Message"] = "string: " + DV_CreateName + "parentId: " + parentId + "DV_TD_ID: " + DV_TD_ID.ToString();
            if (parentId != null && DV_TD_ID != null)
            {
                db.AddNewDV(DV_CreateName, DV_CreateSign, (int)parentId, (int)DV_TD_ID);
                return RedirectToAction("System");
            }
            return View();
        }

        //EditByContextMenu
        [CustomAuthorize("Admin")]
        public ActionResult EditDVByContextMenu()
        {
            ViewBag.Info = TempData["Message"];
            return PartialView("EditDVByContextMenu");
        }

        [HttpPost]
        [CustomAuthorize("Admin")]
        public ActionResult EditDVByContextMenu(int? id, string text, string sign,  int? DV_TD_ID_E)
        {
            TempData["Message"] = "name: " + text + "Id: " + id.ToString() + "sign: " + sign + "DV_TD_ID: " + DV_TD_ID_E.ToString();
            if (id != null && DV_TD_ID_E != null)
            {
                db.EditDVFromParams((int)id, text, sign, (int)DV_TD_ID_E);
                return RedirectToAction("System");
            }
            return View();
        }
        [CustomAuthorize("Admin")]
        public ActionResult DeleteDVByContextMenu()
        {
            ViewBag.Info = TempData["Message"];
            return PartialView("DeleteDVByContextMenu");
        }
        [HttpPost]
        [CustomAuthorize("Admin")]
        public ActionResult DeleteDVByContextMenu(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (db.DeleteDVFromParams((int)id))
            {
                return RedirectToAction("System");
            }
            TempData["Message"] = "Không thể xoá đơn vị do có đơn vị trực thuộc hoặc liên quan dữ liệu";
            return View();
        }

        [HttpPost]
        [CustomAuthorize("Admin")]
        public ActionResult CreateBCFromContexMenu(int? node_id_dv, int? CD_ID, int quantity)
        {
            if ((CD_ID == null) || (node_id_dv == null))
            {
               TempData["Message"] = "CD_ID == null or node_id_dv == null)";
                return RedirectToAction("Respone");
            }
            if (quantity > 0)
            {
                db.AddNewBC((int)node_id_dv, (int)CD_ID, quantity);
                return RedirectToAction("System");
            }
            TempData["Message"] = "No Action";
            return RedirectToAction("Respone");
        }

        [HttpPost]
        [CustomAuthorize("Admin")]
        public ActionResult EditBCByContextMenu(string nodeBCIdE, int quantity)
        {
            if (nodeBCIdE == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            string[] ls = nodeBCIdE.Split('*');
            int bc_id_int = int.Parse(ls[0]);
            db.UpdateBC(bc_id_int, quantity);
            return RedirectToAction("System");
        }

        [HttpPost]
        [CustomAuthorize("Admin")]
        public ActionResult DeleteBCByContextMenu(string nodeId_BC_delete)
        {
            if (nodeId_BC_delete == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            string[] ls = nodeId_BC_delete.Split('*');
            int bc_id_int = int.Parse(ls[0]);
            db.DeleteBC(bc_id_int);
            return RedirectToAction("System");
        }
        #endregion

        #region Setting Connection
        [CustomAuthorize("Admin")]
        public ActionResult UpdateDataSource()
        {
            ViewBag.UserName = "Admin";
            string currentServerAddress = Helper.GetCurrentServerAddress();
            SettingConnection settingConnection = new SettingConnection();
            settingConnection.DataSource = currentServerAddress;
            

            return View(settingConnection);
        }

        [CustomAuthorize("Admin")]
        [HttpPost]
        public ActionResult UpdateDataSource(SettingConnection settingConnection)
        {
            ViewBag.UserName = "Admin";
            // Handle the form submission
            if (ModelState.IsValid)
            {
                // Save data or perform other actions
                Helper.UpdateDataSource(settingConnection.DataSource);
                //lay thong tin ve danh sach 
                

                return View();
            }

            // If the model is invalid, return to the form with errors
            return View(settingConnection);
        }
        [HttpPost]
        public JsonResult VerifyPassword(string password)
        {
            bool isValid = Helper.TestAdminPassword(password);
            return Json(new { isValid });
        }
        #endregion

        #region TaiKhoan User
        [CustomAuthorize("Admin")]
        public ActionResult TaiKhoanIndex()
        {
            List<TaiKhoan_x> list = db.GetListTaiKhoan_X();
            return View(list);
        }
        [CustomAuthorize("Admin")]
        [HttpGet]
        public JsonResult GetDVs(int parentID)
        {
            List<DV> dvs = db.DVs.Where(x=>x.ParentID == parentID).ToList();
            
            return Json(dvs, JsonRequestBehavior.AllowGet);
        }

        [CustomAuthorize("Admin")]
        public ActionResult TaiKhoanCreate()
        {
            TaiKhoan taiKhoan = new TaiKhoan();
            taiKhoan.ID = Helper.GetNewTaiKhoanID(db);
            List<SelectListItem> DV_ID = new List<SelectListItem>();
            DV_ID.Add(new SelectListItem { Value = "-1", Text = "Không gán đơn vị" });
            foreach (var item in db.DVs)
            {
                DV_ID.Add(new SelectListItem { Value = item.ID.ToString(), Text = item.TenDV });
            }
            ViewBag.DV_ID = DV_ID;
            return View(taiKhoan);
        }
        [CustomAuthorize("Admin")]
        [HttpPost]
        public ActionResult TaiKhoanCreate(TaiKhoan model)
        {
            if (db.TestValidTenTaiKhoan(model.TenTK))
            {
               
                db.TaiKhoans.Add(model);
                db.SaveChanges();
                return RedirectToAction("TaiKhoanIndex");
            }
            else
            {
                ViewBag.Msg = "Tên Tài khoản đã tồn tại!";
                return View(model);
            }
        }

        [CustomAuthorize("Admin")]
        public ActionResult TaiKhoanEdit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaiKhoan model = db.TaiKhoans.Find(id);
            if (model == null)
            {
                return HttpNotFound();
            }
            //List<SelectListItem> DV_ID = new List<SelectListItem>();
            //DV_ID.Add(new SelectListItem { Value = "-1", Text = "Không gán đơn vị" });
            //foreach (var item in db.DVs)
            //{
            //    DV_ID.Add(new SelectListItem { Value = item.ID.ToString(), Text = item.TenDV });
            //}

            //if (model.dV_ID != null)
            //{
            //    foreach (var item in DV_ID)
            //    {
            //        if (((int)model.dV_ID).ToString() == item.Value)
            //        {
            //            item.Selected = true;
            //        }
            //    }
            //}
            //ViewBag.DV_ID = DV_ID;

            List<DV> DV_ID = new List<DV>();
            DV_ID.Add(new DV { ID = -1, TenDV = "Không gán"});
            List<DV> DV_ID1 = db.DVs.ToList();
            DV_ID.AddRange(DV_ID1);
            ViewBag.dV_ID = new SelectList(DV_ID, "ID", "TenDV", model.dV_ID);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize("Admin")]
        public ActionResult TaiKhoanEdit([Bind(Include = "ID,TenTK,MK,dV_ID")] TaiKhoan model)
        {
            if (ModelState.IsValid)
            {
                db.Entry(model).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("TaiKhoanIndex");
            }
            ViewBag.Msg = "ModelState not Valid";
            return View(model);
        }
        [CustomAuthorize("Admin")]
        public ActionResult TaiKhoanDelete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaiKhoan model = db.TaiKhoans.Find(id);
            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }
        // POST: CD_TuongDuong/Delete/5
        [HttpPost, ActionName("TaiKhoanDelete")]
        [ValidateAntiForgeryToken]
        [CustomAuthorize("Admin")]
        public ActionResult DeleteTaiKhoanConfirmed(int id)
        {
            TaiKhoan model = db.TaiKhoans.Find(id);
            db.TaiKhoans.Remove(model);
            db.SaveChanges();
            return RedirectToAction("TaiKhoanIndex");
        }

        #endregion
    }
}