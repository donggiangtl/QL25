namespace DataIO.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("HC")]
    public partial class HC
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }

        public int BC_ID { get; set; }

        [Required]
        [StringLength(50)]
        public string HoTen { get; set; }

        [Column(TypeName = "date")]
        public DateTime NgaySinh { get; set; }

        [Column(TypeName = "date")]
        public DateTime NgayBatDau { get; set; }

        [Column(TypeName = "date")]
        public DateTime? NgayKetThuc { get; set; }

        public int BacDaoTao_ID { get; set; }

        public int CapChuyenMon_ID { get; set; }

        public int QH_ID { get; set; }

        public virtual BacDaoTao BacDaoTao { get; set; }

        public virtual BC BC { get; set; }

        public virtual CapChuyenMon CapChuyenMon { get; set; }

        public virtual QH QH { get; set; }
    }
}
