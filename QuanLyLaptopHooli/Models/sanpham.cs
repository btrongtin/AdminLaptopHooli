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
    
    public partial class sanpham
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public sanpham()
        {
            this.chitietdonhangs = new HashSet<chitietdonhang>();
        }
    
        public int id { get; set; }
        public string Ten { get; set; }
        public System.Nullable<int> Gia { get; set; }
        public string HinhAnh { get; set; }
        public string MoTa { get; set; }
        public System.Nullable<int> idThuongHieu { get; set; }
        public System.Nullable<int> SoLuong { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<chitietdonhang> chitietdonhangs { get; set; }
        public virtual thuonghieu thuonghieu { get; set; }
    }
}
