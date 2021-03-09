namespace Framework.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Muon")]
    public partial class Muon
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(5)]
        public string Masinhvien { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(5)]
        public string Masach { get; set; }

        [StringLength(50)]
        public string Hinhthucmuon { get; set; }

        [StringLength(50)]
        public string Ngaymuon { get; set; }

        [StringLength(50)]
        public string Ngaytra { get; set; }

        public int? Songaymuon { get; set; }

        public virtual Sach Sach { get; set; }

        public virtual SinhVien SinhVien { get; set; }
    }
}
