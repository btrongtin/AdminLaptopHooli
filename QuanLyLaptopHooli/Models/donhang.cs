//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace QuanLyLaptopHooli.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class donhang
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public donhang()
        {
            this.chitietdonhangs = new HashSet<chitietdonhang>();
        }
    
        public int id { get; set; }
        public int idKhachHang { get; set; }
        public System.DateTime NgayTao { get; set; }
        public string DiaChiGiaoHang { get; set; }
        public string SoDienThoaiGiaoHang { get; set; }
        public int idTrangThaiGiaoHang { get; set; }
        public int TongTien { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<chitietdonhang> chitietdonhangs { get; set; }
        public virtual khachhang khachhang { get; set; }
        public virtual trangthaigiaohang trangthaigiaohang { get; set; }
    }
}
