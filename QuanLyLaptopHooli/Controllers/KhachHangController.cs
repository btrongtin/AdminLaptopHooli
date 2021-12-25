using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using QuanLyLaptopHooli.Models;

namespace QuanLyLaptopHooli.Controllers
{
    public class KhachHangController : BaseController
    {
        private DBModels db = new DBModels();

        // GET: KhachHang
        public ActionResult Index()
        {
            return View(db.khachhangs.ToList());
        }

        // GET: KhachHang/Details/5
        public ActionResult Details(int? id)
        {
            var kh = db.khachhangs.SingleOrDefault(n => n.id == id);
            if (kh == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(kh);
        }

        // GET: KhachHang/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: KhachHang/Create
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(khachhang kh, FormCollection f)
        {
            if (ModelState.IsValid)
            {
                kh.HoTen = f["sTenKH"];
                kh.TenDangNhap = f["sTenDangNhap"];
                kh.MatKhau = f["sMatKhau"];
                kh.DiaChi = f["sDiaChi"];
                kh.SoDienThoai = f["sSoDienThoai"];

                db.khachhangs.Add(kh);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();

        }



        [HttpGet]
        public ActionResult Delete(int id)
        {
            var kh = db.khachhangs.SingleOrDefault(n => n.id == id);
            if (kh == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(kh);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirm(int id)
        {
            var kh = db.khachhangs.SingleOrDefault(n => n.id == id);
            if (kh == null)
            {
                Response.StatusCode = 404;
                return null;
            }

            db.khachhangs.Remove(kh);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


    }
}
