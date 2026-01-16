using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataIO.MyModel
{
    public class HCTreeNode
    {
        public int ID { get; set; }

        public int BC_ID { get; set; }
        public string HoTen { get; set; }       
    }
    public class BCTreeNode
    {
        public int ID { get; set; }

        public int DV_ID { get; set; }

        public int CD_ID { get; set; }

        public string TenCD { get; set; }

        public int SoLuong { get; set; }
        public List<HCTreeNode> HCs { get; set; } = new List<HCTreeNode>();

        public int SoLuongHC { get
            {
                return HCs.Count;
            }         
        }
    }
   public class DVTreeNode
    {
        public int ID { get; set; }

        public int ParentId {  get; set; }

        public int DV_TD_ID { get; set; }

        public string TenDV { get; set; }
        public string KyHieu { get; set; }
        
        public List<DVTreeNode> Children { get; set; } = new List<DVTreeNode>();
        public List<BCTreeNode> Asset { get; set; } = new List<BCTreeNode>();

        public bool Selected {  get; set; }

        public bool IsOpen { get; set; }



    }
}
