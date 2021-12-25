using QuanLyLaptopHooli.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using System.IO;

namespace QuanLyLaptopHooli.Controllers
{
    public class SanPhamController : BaseController
    {
        DBModels db = new DBModels();
        // GET: SanPham
        public ActionResult Index(int? page)
        {
            int iPageNum = (page ?? 1);
            int iPageSize = 7;
            return View(db.sanphams.ToList().OrderBy(n => n.id).ToPagedList(iPageNum, iPageSize));
        }

        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.MaThuongHieu = new SelectList(db.thuonghieux.ToList().OrderBy(n => n.ten), "id", "ten");

            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(sanpham sp, FormCollection f, HttpPostedFileBase fFileUpload)
        {
            ViewBag.MaDM = new SelectList(db.thuonghieux.ToList().OrderBy(n => n.ten), "id", "ten"); //"MaDM", "TenDM"
            if (fFileUpload == null)
            {
                ViewBag.ThongBao = "Hãy chọn ảnh bìa.";
                ViewBag.TenSP = f["sTenSP"];
                ViewBag.MoTa = f["sMoTa"];
                ViewBag.SoLuong = int.Parse(f["iSoLuong"]);
                ViewBag.GiaBan = decimal.Parse(f["mGiaBan"]);
                ViewBag.MaThuongHieu = new SelectList(db.thuonghieux.ToList().OrderBy(n => n.ten), "id", "ten", int.Parse(f["id"]));
                return View();
            }
            else
            {
                if (ModelState.IsValid)
                {
                    var sFileName = Path.GetFileName(fFileUpload.FileName);
                    var path = Path.Combine(Server.MapPath("~/Images"), sFileName);
                    if (!System.IO.File.Exists(path))
                    {
                        fFileUpload.SaveAs(path);
                    }
                    sp.Ten = f["sTenSP"];
                    sp.MoTa = f["sMoTa"].Replace("<p>", "").Replace("</p>", "\n");
                    sp.HinhAnh = sFileName;
                    sp.SoLuong = int.Parse(f["iSoLuong"]);
                    sp.Gia = int.Parse(f["mGiaBan"]);
                    sp.idThuongHieu = int.Parse(f["MaThuongHieu"]);
                    object p = db.sanphams.Add(sp);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View();
            }
        }

        public ActionResult Details(int id)
        {
            var sp = db.sanphams.SingleOrDefault(n => n.id == id);
            if (sp == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(sp);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var sp = db.sanphams.SingleOrDefault(n => n.id == id);
            if (sp == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(sp);
        }


        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirm(int id)//, FormCollection f
        {
            var sp = db.sanphams.SingleOrDefault(n => n.id == id);
            if (sp == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            //var ctdh = db.CHITIETDATHANGs.Where(ct => ct.MaSP == id);
            //if (ctdh.Count() > 0)
            //{
            //    ViewBag.ThongBao = "Sản phẩm này đang có trong bảng chi tiết đặt hàng <br>" +
            //    "Nếu muốn xóa thì phải xóa hết mã sản phẩm này trong bảng chi tiết đặt hàng";
            //    return View(sp);
            //}

            db.sanphams.Remove(sp);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var sp = db.sanphams.SingleOrDefault(n => n.id == id);
            if (sp == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            ViewBag.MaThuongHieu = new SelectList(db.thuonghieux.ToList().OrderBy(n => n.ten), "id", "ten", sp.idThuongHieu);

            return View(sp);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(FormCollection f, HttpPostedFileBase fFileUpload)
        {
            int idSP = int.Parse(f["iMaSP"]);
            var sp = db.sanphams.SingleOrDefault(n => n.id == idSP);
            ViewBag.MaThuongHieu = new SelectList(db.thuonghieux.ToList().OrderBy(n => n.ten), "id", "ten", sp.idThuongHieu);

            if (ModelState.IsValid)
            {
                if (fFileUpload != null)
                {
                    var sFileName = Path.GetFileName(fFileUpload.FileName);
                    var path = Path.Combine(Server.MapPath("~/Images"), sFileName);
                    if (!System.IO.File.Exists(path))
                    {
                        fFileUpload.SaveAs(path);

                    }
                    sp.HinhAnh = sFileName;
                }
                sp.Ten = f["sTenSP"];
                sp.MoTa = f["sMoTa"].Replace("<p>", "").Replace("</p>", "\n");

                sp.SoLuong = int.Parse(f["iSoLuong"]);
                sp.Gia = int.Parse(f["mGiaBan"]);
                sp.idThuongHieu = int.Parse(f["MaThuongHieu"]);

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(sp);
        }
    }
}