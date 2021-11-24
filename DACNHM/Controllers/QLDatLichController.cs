using DACNHM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
namespace DACNHM.Controllers
{
    public class QLDatLichController : Controller
    {
        dbQLHMDataContext data = new dbQLHMDataContext();
        // GET: Admin
        public ActionResult Index(int? page, string keyword)
        {
            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("Login", "Admin");
            else
            {
                int pagesize =4 ;
                int pagenum = (page ?? 1);
                if (!string.IsNullOrEmpty(keyword))
                {
                    TempData["kwd"] = keyword;
                    List<PhieuDKHienMau> ad = data.PhieuDKHienMaus.Where(n => n.NguoiHienMau.TenNgHien.ToLower().Contains(keyword.ToLower())).ToList();
                    return View(ad.OrderByDescending(n => n.MaPhieuDKHienMau).ToPagedList(pagenum, pagesize));
                }
                return View(data.PhieuDKHienMaus.OrderByDescending(n => n.MaPhieuDKHienMau).ToList().ToPagedList(pagenum, pagesize));
            }
        }
        [HttpPost]
        public ActionResult TimKiem(string keyword)
        {
            if (Session["Taikhoanadmin"] == null)
            {
                return RedirectToAction("Login", "Admin");
            }
            else
            {
                int pagesize = 4;
                int pagenum = 1;

                TempData["kwd"] = keyword;
                List<PhieuDKHienMau> ad = data.PhieuDKHienMaus.Where(n => n.NguoiHienMau.TenNgHien.ToLower().Contains(keyword.ToLower())).ToList();
                return View("Index", ad.OrderByDescending(n => n.MaPhieuDKHienMau).ToPagedList(pagenum, pagesize));
            }
        }

        public ActionResult Edit(String id)
        {

            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("Login", "Admin");
            else
            {
                var admin = from a in data.PhieuDKHienMaus where a.MaPhieuDKHienMau == id select a;
                ViewBag.DotHien = new SelectList(data.DotToChucs.ToList().OrderBy(n => n.DotHien), "DotHien","Ca","NgayToChuc");
                return View(admin.SingleOrDefault());
            }
        }
        [HttpPost, ActionName("Edit")]
        public ActionResult Capnhat(string id)
        {

            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("Login", "Admin");
            else
            {
                PhieuDKHienMau ad = data.PhieuDKHienMaus.SingleOrDefault(n => n.MaPhieuDKHienMau == id);
                UpdateModel(ad);
                data.SubmitChanges();
                return RedirectToAction("Index", "QLDatLich");
            }
        }
        public ActionResult Details(string id)
        {

            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("Login", "Admin");
            else
            {
                var admin = from a in data.PhieuDKHienMaus where a.MaPhieuDKHienMau == id select a;
                return View(admin.Single());
            }
        }
        public ActionResult Delete(string id)
        {

            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("Login", "Admin");
            else
            {
                var admin = from a in data.PhieuDKHienMaus where a.MaPhieuDKHienMau == id select a;
                return View(admin.Single());
            }
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult Xoa(string id)
        {

            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("Login", "Admin");
            else
            {
                PhieuDKHienMau ad = data.PhieuDKHienMaus.SingleOrDefault(n => n.MaPhieuDKHienMau == id);
                data.PhieuDKHienMaus.DeleteOnSubmit(ad);
                data.SubmitChanges();
                return RedirectToAction("Index", "QLDatLich");
            }
        }
    }
}