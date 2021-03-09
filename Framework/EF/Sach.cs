namespace Framework.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Sach")]
    public partial class Sach
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Sach()
        {
            Muons = new HashSet<Muon>();
        }

        [Key]
        [StringLength(5)]
        public string Masach { get; set; }

        [StringLength(50)]
        public string Tensach { get; set; }

        [StringLength(50)]
        public string Namxuatban { get; set; }

        public int? Soluong { get; set; }

        [StringLength(5)]
        public string Mangonngu { get; set; }

        [StringLength(5)]
        public string Manhaxuatban { get; set; }

        [StringLength(5)]
        public string Matacgia { get; set; }

        [StringLength(5)]
        public string Maloai { get; set; }

        [StringLength(5)]
        public string Mavitri { get; set; }

        [StringLength(5)]
        public string Makho { get; set; }

        public virtual Kho Kho { get; set; }

        public virtual LoaiSach LoaiSach { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Muon> Muons { get; set; }

        public virtual NgonNgu NgonNgu { get; set; }

        public virtual NXB NXB { get; set; }

        public virtual TacGia TacGia { get; set; }

        public virtual ViTri ViTri { get; set; }
    }
}
