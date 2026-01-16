namespace DataIO.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TaiKhoan")]
    public partial class TaiKhoan
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        public string TenTK { get; set; }

        [Required]
        [StringLength(50)]
        public string MK { get; set; }

        public int? hC_ID { get; set; }

        public int dV_ID { get; set; }
    }
}
