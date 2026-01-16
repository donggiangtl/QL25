namespace DataIO.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CD")]
    public partial class CD
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CD()
        {
            BCs = new HashSet<BC>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        public string TenCD { get; set; }

        [Required]
        [StringLength(50)]
        public string KyHieu { get; set; }

        public int CD_TD_ID { get; set; }

        public int ChuyenNganhID { get; set; }

        public bool Management { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BC> BCs { get; set; }

        public virtual CD_TuongDuong CD_TuongDuong { get; set; }

        public virtual ChuyenNganh ChuyenNganh { get; set; }
    }
}
