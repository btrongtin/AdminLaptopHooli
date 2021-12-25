using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PagedList;
using QuanLyLaptopHooli.Models;

namespace QuanLyLaptopHooli.Controllers
{
    public class ThuongHieuController : BaseController
    {
        private DBModels data = new DBModels();

        // GET: DanhMuc
        public ActionResult Index(int? page)
        {
            int iPageNum = (page ?? 1);
            int iPageSize = 7;
            return View(data.thuonghieux.ToList().OrderBy(n => n.id).ToPagedList(iPageNum, iPageSize));
        }

        public ActionResult Details(int id)
        {
            var sp = data.thuonghieux.SingleOrDefault(n => n.id == id);
            if (sp == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(sp);
        }

        public thuonghieu GetTH(int id)
        {
            return (data.thuonghieux.OrderBy(n => n.ten).Where(n => n.id == id).SingleOrDefault());
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            //NHAXUATBAN nxb = data.NHAXUATBANs.Where(n => n.MaNXB == id).SingleOrDefault();
            //return View(nxb);
            return View(GetTH(id));
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(FormCollection f, HttpPostedFileBase fFileUpload)
        {
            int idTH = int.Parse(f["iMaTH"]);
            var th = GetTH(idTH);
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
                    th.hinhanh = sFileName;
                }

                th.ten = f["sTenTH"];
                

                data.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(th);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            return View(GetTH(id));
        }

        [HttpPost/*, ActionName("Delete")*/]
        public ActionResult Delete/*Confirm*/(int id, FormCollection f)
        {
            var th = GetTH(id);
            if (th == null)
            {
                Response.StatusCode = 404;
                return null;
            }

            data.thuonghieux.Remove(th);
            data.SaveChanges();
            return RedirectToAction("Index");
        }


        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(thuonghieu th, FormCollection f, HttpPostedFileBase fFileUpload)
        {
            if (fFileUpload == null)
            {
                ViewBag.ThongBao = "Hãy chọn ảnh bìa.";
                ViewBag.TenTH = f["TenTH"];
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
                    th.ten = f["TenTH"];
                    th.hinhanh = sFileName;

                    data.thuonghieux.Add(th);
                    data.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View();
            }

        }
    }
}
