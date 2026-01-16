using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataIO.MyModels
{
    public class SettingConnection
    {
        public string DataSource { get; set; }

        [DataType(DataType.MultilineText)]
        public string ConnectString { get; set; }
        public string EnviromentVariable { get; set; }
    }
}
