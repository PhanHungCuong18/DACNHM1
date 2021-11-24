using DACNHM.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DACNHM.Controllers
{
    public class QLDotHienController : Controller
    {
        dbQLHMDataContext data = new dbQLHMDataContext();

        public ActionResult Index(int? page, string keyword)
        {
            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("Login", "Admin");
            else
            {
                int pagesize = 3;
                int pagenum = (page ?? 1);
                if (!string.IsNullOrEmpty(keyword))
                {
                    TempData["kwd"] = keyword;
                    List<DotToChuc> ad = data.DotToChucs.Where(n => n.DiaDiemHienMau.TenDiaDiem.ToLower().Contains(keyword.ToLower())).ToList();
                    return View(ad.OrderByDescending(n => n.DotHien).ToPagedList(pagenum, pagesize));
                }
                return View(data.DotToChucs.OrderByDescending(n => n.DotHien).ToList().ToPagedList(pagenum, pagesize));
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
                List<DotToChuc> ad = data.DotToChucs.Where(n => n.DiaDiemHienMau.TenDiaDiem.ToLower().Contains(keyword.ToLower())).ToList();
                return View("Index", ad.OrderByDescending(n => n.DotHien).ToPagedList(pagenum, pagesize));
            }
        }
        public ActionResult create()
        {

            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("Login", "Admin");
            else
            {
                ViewBag.MaDiaDiem = new SelectList(data.DiaDiemHienMaus.ToList().OrderBy(n => n.TenDiaDiem), "MaDiaDiem", "TenDiaDiem");
                ViewBag.MaDonViQL = new SelectList(data.QuanLyHienMaus.ToList().OrderBy(n => n.TenDonViQL), "MaDonViQL", "TenDonViQL");
                return View();
            }
        }
        [HttpPost]
        public ActionResult create(DotToChuc ad)
        {

            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("Login", "Admin");
            else
            {
                data.DotToChucs.InsertOnSubmit(ad);

                data.SubmitChanges();
                return RedirectToAction("Index", "QLDotHien");
            }
        }
        public ActionResult Edit(int id)
        {

            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("Login", "Admin");
            else
            {
                var admin = from a in data.DotToChucs where a.DotHien == id select a;
                ViewBag.MaDiaDiem = new SelectList(data.DiaDiemHienMaus.ToList().OrderBy(n => n.TenDiaDiem), "MaDiaDiem", "TenDiaDiem");
                ViewBag.MaDonViQL = new SelectList(data.QuanLyHienMaus.ToList().OrderBy(n => n.TenDonViQL), "MaDonViQL", "TenDonViQL");
                return View(admin.SingleOrDefault());
            }
        }
        [HttpPost, ActionName("Edit")]
        public ActionResult Capnhat(int id)
        {

            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("Login", "Admin");
            else
            {
                DotToChuc ad = data.DotToChucs.SingleOrDefault(n => n.DotHien == id);
                UpdateModel(ad);
                data.SubmitChanges();
                return RedirectToAction("Index", "QLDotHien");
            }
        }
        public ActionResult Details(int id)
        {

            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("Login", "Admin");
            else
            {
                var admin = from a in data.DotToChucs where a.DotHien == id select a;
                return View(admin.Single());
            }
        }
        public ActionResult Delete(int id)
        {

            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("Login", "Admin");
            else
            {
                var admin = from a in data.DotToChucs where a.DotHien == id select a;
                return View(admin.Single());
            }
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult Xoa(int id)
        {

            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("Login", "Admin");
            else
            {
                DotToChuc ad = data.DotToChucs.SingleOrDefault(n => n.DotHien == id);
                data.DotToChucs.DeleteOnSubmit(ad);
                data.SubmitChanges();
                return RedirectToAction("Index", "QLDotHien");
            }
        }
    }
}