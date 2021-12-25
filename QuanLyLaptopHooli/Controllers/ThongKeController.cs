using QuanLyLaptopHooli.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuanLyLaptopHooli.Controllers
{
    public class ThongKeController : BaseController
    {
        DBModels data = new DBModels();

        public ActionResult Thongke()
        {
            var kq = from sp in data.sanphams
                     join th in data.thuonghieux on sp.idThuongHieu equals th.id
                     group sp by sp.idThuongHieu into g
                     select new ReportInfo
                     {

                         Id = g.Key.ToString(),
                         Name = g.FirstOrDefault().thuonghieu.ten,
                         Count = g.Count(),
                         Sum = g.Sum(n => n.SoLuong),
                         Max = g.Max(n => n.SoLuong),
                         Min = g.Min(n => n.SoLuong),
                         //Avg = Convert.ToDecimal(g.Average(n => n.SoLuong))
                     };
            return PartialView(kq);
        }

        public ActionResult Group()
        {
            var kq = data.sanphams.GroupBy(s => s.idThuongHieu).ToList();
            return View(kq);
        }

        [HttpGet]
        public ActionResult DoanhThu()
        {
            DateTime today = DateTime.Now;
            var listDonHang = data.donhangs.ToList().Where(s => s.NgayTao.Month == today.Month);
            var tongDoanhThu = data.donhangs.Where(s => s.NgayTao.Month == today.Month).Sum(a => a.TongTien);
            var kq = new DoanhThuViewModel(tongDoanhThu, listDonHang);

            return PartialView(kq);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult DoanhThu(FormCollection f)
        {
            DateTime fromDate = DateTime.ParseExact(f["fromDate"], "dd/MM/yyyy", null);
            DateTime toDate = DateTime.ParseExact(f["toDate"], "dd/MM/yyyy", null);
            var listDonHang = data.donhangs.ToList().Where(s => s.NgayTao >= fromDate && s.NgayTao <= toDate);
            var tongDoanhThu = data.donhangs.Where(s => s.NgayTao >= fromDate && s.NgayTao <= toDate).Sum(a => a.TongTien);
            var kq = new DoanhThuViewModel(tongDoanhThu, listDonHang);


            return PartialView(kq);
        }
    }
}