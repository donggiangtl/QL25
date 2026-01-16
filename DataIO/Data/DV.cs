namespace DataIO.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DV")]
    public partial class DV
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DV()
        {
            BCs = new HashSet<BC>();
            DV1 = new HashSet<DV>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }

        [StringLength(50)]
        public string TenDV { get; set; }

        [StringLength(50)]
        public string KyHieu { get; set; }

        public int DV_TD_ID { get; set; }

        public bool? IsOpen { get; set; }

        public int ParentID { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BC> BCs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DV> DV1 { get; set; }

        public virtual DV DV2 { get; set; }

        public virtual DV_TuongDuong DV_TuongDuong { get; set; }
    }
}
