using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataIO.MyModel
{
    public class TaiKhoan_x
    {
        public int ID { get; set; }
        public string TenTK { get; set; }
        public string MK { get; set; }
        public int? hC_ID { get; set; }

        public int dV_ID { get; set; }
        public string TenHC { get; set; }
        public string TenDV { get; set; }
    }
}
