namespace Framework.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ViTri")]
    public partial class ViTri
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ViTri()
        {
            Saches = new HashSet<Sach>();
        }

        [Key]
        [StringLength(5)]
        public string Mavitri { get; set; }

        [StringLength(50)]
        public string Khu { get; set; }

        [StringLength(50)]
        public string Ke { get; set; }

        [StringLength(50)]
        public string Ngan { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Sach> Saches { get; set; }
    }
}
