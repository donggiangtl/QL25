using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataIO.MyModel
{
   public class HC_ShowModel
    {
        public int ID { get; set; }

        public int BC_ID { get; set; }
        public string HoTen { get; set; }
        public DateTime NgaySinh { get; set; }
        public DateTime NgayBatDau { get; set; }
        public DateTime? NgayKetThuc { get; set; }
        public int BacDaoTao_ID { get; set; }

        public int CapChuyenMon_ID { get; set; }

        public int QH_ID { get; set; }
        public string TenCD { get; set; }
        public string CapHam { get; set; }
        public string TenDV { get; set; }

        public int intParam_ID { get; set; }
    }
}
