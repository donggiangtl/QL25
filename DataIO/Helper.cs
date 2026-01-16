using DataIO.Data;
using DataIO.MyModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;
using System.Web.UI.WebControls;
using static System.Net.Mime.MediaTypeNames;

namespace DataIO
{
    public enum TestAccount{
        ok,
        wrong_username,
        wrong_password,
        disable
    }
    public static class Helper
    {
        #region setting connection
        static public string GetCurrentServerAddress()
        {
            // Get the connection string from web.config
            var connectionString = ConfigurationManager.ConnectionStrings["MyDB"].ConnectionString;

            // Use SqlConnectionStringBuilder to extract the server address
            var builder = new SqlConnectionStringBuilder(connectionString);
            return builder.DataSource; // Returns the server address
        }

        static public string GetCurrentConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["MyDB"].ConnectionString;
        }

        static public void UpdateDataSource(string newDataSource)
        {
            Configuration config = WebConfigurationManager.OpenWebConfiguration("~");
            ConnectionStringsSection section = config.GetSection("connectionStrings") as ConnectionStringsSection;

            if (section != null)
            {
                // Get the existing connection string
                var connectionString = section.ConnectionStrings["MyDB"].ConnectionString;

                // Update the data source in the connection string
                var builder = new System.Data.SqlClient.SqlConnectionStringBuilder(connectionString);
                builder.DataSource = newDataSource; // Update the data source (server address)
                section.ConnectionStrings["MyDB"].ConnectionString = builder.ConnectionString;

                // Save the changes
                config.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection("connectionStrings");
            }
        }


        public static void UpdateConnectionString(string newConnectionString)
        {
            Configuration config = WebConfigurationManager.OpenWebConfiguration("~");
            ConnectionStringsSection section = config.GetSection("connectionStrings") as ConnectionStringsSection;

            if (section != null)
            {
                section.ConnectionStrings["MyDB"].ConnectionString = newConnectionString;
                config.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection("connectionStrings");
            }
        }

        public static bool TestValidCurrentSonnectionString()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MyDB"].ConnectionString;
            if (Helper.DatabaseExists(connectionString))
                return true;
            else
                return false;
        }
        public static bool DatabaseExists(string connectionString)
        {
            var builder = new SqlConnectionStringBuilder(connectionString);
            string databaseName = builder.InitialCatalog;

            // Temporarily change to master to check if DB exists
            builder.InitialCatalog = "master";

            string query = "SELECT COUNT(*) FROM sys.databases WHERE name = @dbName";

            using (var connection = new SqlConnection(builder.ConnectionString))
            using (var command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@dbName", databaseName);
                try
                {
                    connection.Open();
                    int count = (int)command.ExecuteScalar();
                    return count > 0;
                }
                catch (Exception ex)
                {
                    // Log exception if needed
                    Console.WriteLine(ex.Message);
                    return false;
                }
            }
        }

        static public void StoreDataToEnviromentVariable(string name, string data)
        {
            Environment.SetEnvironmentVariable(name, data, EnvironmentVariableTarget.User);
        }
        #endregion

        #region taikhoan admin
        static public PassWordModel GetAdminPassWordModel()
        {
            return new PassWordModel { UerName = Environment.GetEnvironmentVariable("ql25_username", EnvironmentVariableTarget.Machine), PassWord = Environment.GetEnvironmentVariable("ql25_password", EnvironmentVariableTarget.Machine) };
        }
        static public TestAccount TestAdminAccount(PassWordModel passWordModel)
        {
            string stored_username = Environment.GetEnvironmentVariable("ql25_username", EnvironmentVariableTarget.Machine);
            if (stored_username == null || stored_username.Length == 0)
                return TestAccount.disable;
            if (stored_username.Trim() != passWordModel.UerName.Trim())
                return TestAccount.wrong_username;
            string stored_password = Environment.GetEnvironmentVariable("ql25_password", EnvironmentVariableTarget.Machine);
            if (stored_password == null || stored_password.Length == 0)
                return TestAccount.disable;
            if (stored_password.Trim() != passWordModel.PassWord.Trim())
                return TestAccount.wrong_password;
            return TestAccount.ok;
        }
        static public TestAccount TestUserAccount(PassWordModel passWordModel, out int dV_ID)
        {
            MyDB db = new MyDB();
            TaiKhoan taiKhoan = db.TaiKhoans.Where(x=>x.TenTK == passWordModel.UerName && x.MK == passWordModel.PassWord).FirstOrDefault();
            if (taiKhoan == null)
            {
                dV_ID = -1;
                return TestAccount.wrong_username;
            }
            dV_ID = taiKhoan.dV_ID;
            return TestAccount.ok;
        }

        
        public static bool TestAdminPassword(string password)
        {
            return password == Helper.GetAdminPassWordModel().PassWord;
        }
        #endregion

        #region cac ham dung chung
        /// <summary>
        /// Lấy ID hợp lệ nhỏ nhất từ danh sách ID cho trước. Giá trị này không xuất hiện trong danh sách
        /// </summary>
        /// <param name="list">Danh sách đầu vào</param>
        /// <returns>Giá trị ID nhỏ nhất hợp lệ</returns>
        public static int GetID(List<int> list)
        {
            if (list.Count == 0)
            {
                return 0;
            }
            int min, max;
            min = list[0];
            if (min > 0)
            {
                min = 0;
            }
            max = list[0];
            foreach (int item in list)
            {
                if (min > item)
                {
                    min = item;
                }
                if (max < item)
                {
                    max = item;
                }
            }
            if (min == max)
            {
                return max + 1;
            }
            int i = min;
            while (list.Contains(i) && i <= max)
            {
                i++;
            }
            return i;
        }


        #endregion

        #region Chuc danh tuong duong
        public static int GetNewChucDanhTuongDuongID(MyDB db)
        {
            List<int> listID = new List<int>();

            foreach (var item in db.CD_TuongDuong)
            {
                listID.Add(item.ID);
            }
            return Helper.GetID(listID);
        }
        #endregion

        #region Don vi tuong duong
        public static int GetNewDonViTuongDuongID(MyDB db)
        {
            List<int> listID = new List<int>();

            foreach (var item in db.DV_TuongDuong)
            {
                listID.Add(item.ID);
            }
            return Helper.GetID(listID);
        }
        #endregion

        #region Nganh
        public static int GetNewNganhID(MyDB db)
        {
            List<int> listID = new List<int>();

            foreach (var item in db.Nganhs)
            {
                listID.Add(item.ID);
            }
            return Helper.GetID(listID);
        }
        #endregion

        #region Chuyen nganh
        public static int GetNewChuyenNganhID(MyDB db)
        {
            List<int> listID = new List<int>();

            foreach (var item in db.ChuyenNganhs)
            {
                listID.Add(item.ID);
            }
            return Helper.GetID(listID);
        }
        #endregion

        #region Tai khoan user
        public static int GetNewTaiKhoanID(MyDB db)
        {
            List<int> listID = new List<int>();

            foreach (var item in db.TaiKhoans)
            {
                listID.Add(item.ID);
            }
            return Helper.GetID(listID);
        }
        /// <summary>
        /// Lấy tên đơn vị được gán cho tài khoản
        /// </summary>
        /// <param name="db"></param>
        /// <param name="id">id của HC</param>
        /// <returns></returns>
        static public string GetTenDV(this MyDB db, int id)
        {
           DV dV = db.DVs.Find(id);
            if (dV == null)
                return string.Empty;
            return dV.TenDV;
        }

        static public string GetTenHC(this MyDB db, int id)
        {
           HC hC = db.HCs.Find( id);
            if (hC == null)
                return string.Empty;
            return hC.HoTen;
        }

        public static bool TestValidTenTaiKhoan(this MyDB db, string tenTK)
        {
            TaiKhoan taiKhoan = db.TaiKhoans.Where(x => x.TenTK == tenTK).FirstOrDefault();
            if (taiKhoan == null) { return true; }
            return false;
        }
        public static bool TestValidTenTaiKhoanEdit(this MyDB db, string tenTK)
        {
            TaiKhoan taiKhoan = db.TaiKhoans.Where(x => x.TenTK == tenTK).FirstOrDefault();
            if (taiKhoan == null) { return true; }
            return false;
        }

        static public List<TaiKhoan> GetListTaiKhoan(this MyDB db)
        {
            return db.TaiKhoans.ToList();
        }

        static public TaiKhoan_x GetTaiKhoan_X(this MyDB db, TaiKhoan taiKhoan)
        {
            TaiKhoan_x taiKhoan_X = new TaiKhoan_x
            {
                ID = taiKhoan.ID,
                TenTK = taiKhoan.TenTK,
                MK = taiKhoan.MK,
                hC_ID = taiKhoan.hC_ID,
                dV_ID = taiKhoan.dV_ID
            };

            if (taiKhoan.dV_ID != -1)
            {
                DV dV = db.DVs.Find(taiKhoan.dV_ID);
                if (dV != null)
                    taiKhoan_X.TenDV = dV.TenDV;
                else
                    taiKhoan_X.TenDV = "Không xác định";
            }
            else
                taiKhoan_X.TenDV = "Không được gán";
            if (taiKhoan.hC_ID != null)
            {
                HC hC = db.HCs.Find(taiKhoan.hC_ID);
                if (hC != null)
                {
                    taiKhoan_X.TenHC = hC.HoTen;
                }
            }

            return taiKhoan_X;
        }

        static public List<TaiKhoan_x> GetListTaiKhoan_X(this MyDB db)
        {
            List<TaiKhoan_x> list = new List<TaiKhoan_x>();
            foreach (var item in db.TaiKhoans)
            {
                list.Add(db.GetTaiKhoan_X(item));
            }
            return list;
        }

        #endregion

        #region Chuc danh
        public static int GetNewChucDanhID(MyDB db)
        {
            List<int> listID = new List<int>();

            foreach (var item in db.CDs)
            {
                listID.Add(item.ID);
            }
            return Helper.GetID(listID);
        }
        #endregion

        #region BacDaoTao
        public static int GetNewBacDaoTaoID(MyDB db)
        {
            List<int> listID = new List<int>();

            foreach (var item in db.BacDaoTaos)
            {
                listID.Add(item.ID);
            }
            return Helper.GetID(listID);
        }
        #endregion

        #region CapChuyenMon
        public static int GetNewCapChuyenMonID(MyDB db)
        {
            List<int> listID = new List<int>();

            foreach (var item in db.CapChuyenMons)
            {
                listID.Add(item.ID);
            }
            return Helper.GetID(listID);
        }

        #endregion

        #region Khoi tao du lieu
        static public void ResetDatabase(MyDB db)
        {
            db.Database.ExecuteSqlCommand("EXEC ResetDatabase");
            db.Database.ExecuteSqlCommand("EXEC KhoiTaoDuLieuCoSo");
        }
        #endregion

        #region CanBoNhanVien

        #endregion

        #region AdminGetSystem
       
        public static HCTreeNode HCToTreeNode(this HC hC)
        {
            return new HCTreeNode { 
            ID = hC.ID,
            BC_ID = hC.BC_ID,
            HoTen = hC.HoTen,            
            };
        }

        public static BCTreeNode BCToTreeNode(this BC bC)
        {
            List<HCTreeNode> hCTreeNodes = new List<HCTreeNode>();
            foreach (var item in bC.HCs)
            {
                hCTreeNodes.Add(item.HCToTreeNode());
            }
            return new BCTreeNode { 
            ID=bC.ID,
            DV_ID = bC.DV_ID,
            CD_ID = bC.CD_ID,
            TenCD = bC.CD.TenCD,
            SoLuong = bC.SoLuong,
            HCs = hCTreeNodes
            };
        }



        static public DVTreeNode GetDVSystem(this MyDB db, int dv_id)
        {
            DV dV = db.DVs.Find(dv_id);
            if (dV != null)
            {
                List<BCTreeNode> lst = new List<BCTreeNode>();
                foreach (var item in dV.BCs)
                {
                    lst.Add(item.BCToTreeNode());
                }
                DVTreeNode dVTreeNode = new DVTreeNode
                {
                    ID = dv_id,
                    ParentId = dV.ParentID,
                    TenDV = dV.TenDV,
                    KyHieu = dV.KyHieu,
                    Asset = lst,
                    Selected = false,
                    IsOpen = dV.IsOpen != null && dV.IsOpen.Value,
                    DV_TD_ID = dV.DV_TD_ID
                };

                if (dV.DV1 != null)
                {                    
                    List<DVTreeNode> lst1 = new List<DVTreeNode>();
                    foreach (var item1 in dV.DV1)
                    {
                        if (item1.ID != dV.ID)// do root có children là chính nó, do vậy cần điều kiện để dừng lặp
                        {
                            lst1.Add(db.GetDVSystem(item1.ID));
                        }                        
                    }
                    dVTreeNode.Children = lst1;
                }
                return dVTreeNode;
            }
            
            return null;
        }

        static public bool DeleteDV(this MyDB db, int dv_id)
        {
            if (Helper.CanDeleteDV(db, dv_id))
            {
                DV dV = db.DVs.Find(dv_id);
                db.DVs.Remove(dV);
                db.SaveChanges();
                return true;
            }
            return false;
        }

        static public void UpdateDVTreeNodeState(this MyDB db, int treeNode_Id, bool isOpen)
        {
            DV dV = db.DVs.Find(treeNode_Id);
            if (dV != null)
            {
                dV.IsOpen = isOpen;
                db.Entry(dV).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        static public void UpdateBCC_x(this MyDB db, string treeNode_Id, int quantity)
        {
            
        }

        #endregion

        #region DV
        public static int GetNewDV_ID(this MyDB db)
        {
            List<int> listID = new List<int>();

            foreach (var item in db.DVs)
            {
                listID.Add(item.ID);
            }
            return Helper.GetID(listID);
        }
        
        static public bool CanDeleteDV(MyDB db, int id)
        {
            DV dV = db.DVs.Find(id);
            if (dV.BCs.Count > 0) { return false; }
            return true;
        }
        
        static public bool AddNewDV(this MyDB db, string TenDV, string KyHieu, int parentID, int dV_TD_ID)
        {
            DV parentDV = db.DVs.Find(parentID);
            if (parentDV == null)
                return false;
            DV_TuongDuong dV_TuongDuong = db.DV_TuongDuong.Find(dV_TD_ID);
            if (dV_TuongDuong == null)
                return false;
            DV dV = new DV{
                ID = db.GetNewDV_ID(),
                TenDV = TenDV,
                KyHieu = KyHieu,
                ParentID = parentID,
                DV_TD_ID = dV_TD_ID,
                IsOpen = false
            };
            db.DVs.Add(dV);
            db.SaveChanges();
            return true;
        }

        static public void EditDVFromParams(this MyDB db, int dv_id, string TenDV, string KyHieuDV, int dv_td)
        {
            DV dV = db.DVs.Find(dv_id);
            if (dV != null)
            {
                dV.TenDV = TenDV;
                dV.KyHieu = KyHieuDV;
                dV.DV_TD_ID = dv_td;
                db.Entry(dV).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        static public bool DeleteDVFromParams(this MyDB db, int dv_id)
        {
            if (Helper.CanDeleteDV(db, dv_id))
            {
                DV dv = db.DVs.Find(dv_id) as DV;
                db.DVs.Remove(dv);
                db.SaveChanges();
                return true;
            }
            return false;
        }

        #endregion

        #region BC
        public static int GetNewBC_ID(MyDB db)
        {
            List<int> listID = new List<int>();

            foreach (var item in db.BCs)
            {
                listID.Add(item.ID);
            }
            return Helper.GetID(listID);
        }
        public static bool AddNewBC(this MyDB db, int dv_Id, int cd_ID, int quantity)
        {
            if (db.BCs.Where(x => x.DV_ID == dv_Id && x.CD_ID == cd_ID).ToList().Count > 0)
                return false;
            if (quantity <= 0)
                return false;
            db.BCs.Add(new BC    { 
                ID = Helper.GetNewBC_ID(db),
                DV_ID = dv_Id,
                CD_ID = cd_ID, SoLuong = quantity
            });
            db.SaveChanges();
            return true;
        }
        public static bool UpdateBC(this MyDB db, int bc_id, int quantity)
        {
            BC bC = db.BCs.Find(bc_id);
            if (bC == null) return false;
            if (quantity <= 0) return false;
            bC.SoLuong = quantity;
            db.Entry(bC).State = EntityState.Modified;
            db.SaveChanges();
            return true;
        }

        public static bool DeleteBC(this MyDB db, int bc_id)
        {
            BC bC = db.BCs.Find(bc_id);
            if (bC == null) return false;
            db.BCs.Remove(bC);
            db.SaveChanges();
            return true;
        }
        #endregion

        #region HC
        public static int GetNewHC_ID(this MyDB db)
        {
            List<int> listID = new List<int>();

            foreach (var item in db.HCs)
            {
                listID.Add(item.ID);
            }
            return Helper.GetID(listID);
        }

        public static bool AddNewHC(this MyDB db, int BC_ID, string HoTen, DateTime NgaySinh, DateTime NgayBatDau, DateTime? NgayKetThuc, int BacDaoTao_ID, int CapChuyenMon_ID, int QH_ID)
        {
           HC hC = new HC { 
               ID = db.GetNewHC_ID(),
               BC_ID = BC_ID,
               HoTen = HoTen,
               NgaySinh = NgaySinh,
               NgayBatDau = NgayBatDau,
               NgayKetThuc = NgayKetThuc,
               BacDaoTao_ID = BacDaoTao_ID,
               CapChuyenMon_ID = CapChuyenMon_ID,
               QH_ID = QH_ID
           };
            db.HCs.Add(hC);
            db.SaveChanges();
            return true;
        }
        public static bool EditHC(this MyDB db, int id, int BC_ID, string HoTen, DateTime NgaySinh, DateTime NgayBatDau, DateTime? NgayKetThuc, int BacDaoTao_ID, int CapChuyenMon_ID, int QH_ID)
        {
            HC hC = db.HCs.Find(id);
            if (hC == null)
                return false;

            hC.BC_ID = BC_ID;
            hC.HoTen = HoTen;
            hC.NgaySinh = NgaySinh;
            hC.NgayBatDau = NgayBatDau;
            hC.NgayKetThuc = NgayKetThuc;
            hC.BacDaoTao_ID = BacDaoTao_ID;
            hC.CapChuyenMon_ID = CapChuyenMon_ID;
            hC.QH_ID = QH_ID;
            db.Entry(hC).State = EntityState.Modified;
            db.SaveChanges();
            return true;
        }

        public static List<HC> GetHCsByDV(this MyDB db, int dv_id)
        {
            return db.HCs.Where(x=>x.BC.DV_ID == dv_id).ToList();
        }

        public static  HC_ShowModel HC2HC_ShowMode(this MyDB db, HC hC)
        {
            return new HC_ShowModel { 
                ID = hC.ID,
                HoTen = hC.HoTen,
                CapHam = hC.QH.CapHam,
                TenCD = hC.BC.CD.TenCD,
                TenDV = hC.BC.DV.TenDV,
                BC_ID = hC.BC.ID,
                CapChuyenMon_ID = hC.CapChuyenMon_ID,
                BacDaoTao_ID = hC.BacDaoTao_ID,
                QH_ID = hC.QH_ID
            };
        }

        public static void UpdateFromModel(this HC hC, HC_ShowModel model)
        {
            hC.HoTen = model.HoTen;
            hC.BC_ID = model.BC_ID;
            hC.BacDaoTao_ID= model.BacDaoTao_ID;
            hC.CapChuyenMon_ID = model.CapChuyenMon_ID;
            hC.QH_ID = model.QH_ID;
            hC.NgaySinh = model.NgaySinh;
            hC.NgayBatDau = model.NgayBatDau;
            hC.NgayKetThuc =    model.NgayKetThuc;
        }
        public static List<HC_ShowModel> GetHC_ShowModelsByDV(this MyDB db, int dv_id)
        {
            List<HC_ShowModel> l = new List<HC_ShowModel>();
            List<HC> h = db.HCs.Where(x => x.BC.DV_ID == dv_id).ToList();
            foreach (var item in h)
            {
                l.Add(db.HC2HC_ShowMode(item));
            }
            return l;
        }
        public static List<HC_ShowModel> GetHC_ShowModelsByDVAll(this MyDB db, int dv_id)
        {
            List<HC_ShowModel> l = new List<HC_ShowModel>();
            
            List<HC> h = db.HCs.Where(x => x.BC.DV_ID == dv_id).ToList();
            foreach (var item in h)
            {
                l.Add(db.HC2HC_ShowMode(item));
            }
            List<DV> children = db.DVs.Where(x => x.ParentID == dv_id).ToList();
            if (children.Count > 0)
            {
                foreach (var item in children)
                {
                    l.AddRange(db.GetHC_ShowModelsByDVAll(item.ID));
                }
            }
            return l;
        }
        #endregion 
    }
}
