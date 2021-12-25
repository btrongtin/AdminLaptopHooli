using PagedList;
using QuanLyLaptopHooli.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuanLyLaptopHooli.Controllers
{
    public class DonHangController : BaseController
    {
        DBModels db = new DBModels();
        // GET: DonHang
        public ActionResult Index(int? page)
        {
            int iPageNum = (page ?? 1);
            int iPageSize = 7;
            return View(db.donhangs.ToList().OrderByDescending(n => n.id).ToPagedList(iPageNum, iPageSize));
        }

        public ActionResult ChiTietDonHangPartial(int id)
        {
            var kq = db.chitietdonhangs.Where(a => a.idDonHang == id).ToList();
            return PartialView(kq);
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            var dh = db.donhangs.SingleOrDefault(n => n.id == id);
            if (dh == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            ViewBag.MaTrangThaiGiaoHang = new SelectList(db.trangthaigiaohangs.ToList().OrderBy(n => n.TenTrangThai), "id", "TenTrangThai", dh.idTrangThaiGiaoHang); 
            return View(dh);
        }


        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Details(FormCollection f)
        {
            int idDH = int.Parse(f["iMaDH"]);
            var dh = db.donhangs.SingleOrDefault(n => n.id == idDH);
            ViewBag.MaTrangThaiGiaoHang = new SelectList(db.trangthaigiaohangs.ToList().OrderBy(n => n.TenTrangThai), "id", "TenTrangThai", dh.idTrangThaiGiaoHang);

            if (ModelState.IsValid)
            {

                dh.idTrangThaiGiaoHang = int.Parse(f["MaTrangThaiGiaoHang"]);

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(dh);
        }

    }
}