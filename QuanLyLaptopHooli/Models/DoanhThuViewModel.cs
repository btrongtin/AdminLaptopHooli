using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuanLyLaptopHooli.Models
{
    public class DoanhThuViewModel
    {
        public long TongTien { get; set; }
        public IEnumerable<donhang> DanhSachDonHang { get; set; }

        public DoanhThuViewModel(long tongTien, IEnumerable<donhang> danhSachDonHang)
        {
            TongTien = tongTien;
            DanhSachDonHang = danhSachDonHang;
        }
    }
}