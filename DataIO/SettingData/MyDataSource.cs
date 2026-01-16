using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace DataIO.SettingData
{
    public partial class MyDataSource : DbContext
    {
        public MyDataSource()
            : base("name=MyDataSource")
        {
        }

        public virtual DbSet<DataSourceAdress> DataSourceAdresses { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
