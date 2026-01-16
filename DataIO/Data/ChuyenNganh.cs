namespace DataIO.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ChuyenNganh")]
    public partial class ChuyenNganh
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ChuyenNganh()
        {
            CDs = new HashSet<CD>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }

        public int NganhID { get; set; }

        [Required]
        [StringLength(50)]
        public string TenChuyenNganh { get; set; }

        [StringLength(50)]
        public string NhomNganh { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CD> CDs { get; set; }

        public virtual Nganh Nganh { get; set; }
    }
}
