using DACNHM.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DACNHM.Controllers
{
    public class TTSKController : Controller
    {
        dbQLHMDataContext data = new dbQLHMDataContext();
        // GET: TTSK
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
                    List<PhieuTinhTrangSucKhoe> ad = data.PhieuTinhTrangSucKhoes.Where(n => n.NguoiHienMau.TenNgHien.ToLower().Contains(keyword.ToLower())).ToList();
                    return View(ad.OrderByDescending(n => n.MaPhieuTTSK).ToPagedList(pagenum, pagesize));
                }
                return View(data.PhieuTinhTrangSucKhoes.OrderByDescending(n => n.MaPhieuTTSK).ToList().ToPagedList(pagenum, pagesize));
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
                List<PhieuTinhTrangSucKhoe> ad = data.PhieuTinhTrangSucKhoes.Where(n => n.NguoiHienMau.TenNgHien.ToLower().Contains(keyword.ToLower())).ToList();
                return View("Index", ad.OrderByDescending(n => n.MaPhieuTTSK).ToPagedList(pagenum, pagesize));
            }
        }
        public ActionResult create()
        {

            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("Login", "Admin");
            else
            {
                ViewBag.MaTTSK = new SelectList(data.TinhTrangSucKhoes.ToList().OrderBy(n => n.LoaiTTSK), "MaTTSK", "LoaiTTSK");
                return View();
            }
        }
        [HttpPost]
        public ActionResult create(PhieuTinhTrangSucKhoe ad)
        {

            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("Login", "Admin");
            else
            {
                data.PhieuTinhTrangSucKhoes.InsertOnSubmit(ad);

                data.SubmitChanges();
                return RedirectToAction("Index", "TTSK");
            }
        }
        public ActionResult Edit(string id)
        {

            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("Login", "Admin");
            else
            {
                var admin = from a in data.PhieuTinhTrangSucKhoes where a.MaPhieuTTSK == id select a;
                ViewBag.MaTTSK = new SelectList(data.TinhTrangSucKhoes.ToList().OrderBy(n => n.LoaiTTSK), "MaTTSK", "LoaiTTSK");
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
                PhieuTinhTrangSucKhoe ad = data.PhieuTinhTrangSucKhoes.SingleOrDefault(n => n.MaPhieuTTSK == id);
                UpdateModel(ad);
                data.SubmitChanges();
                return RedirectToAction("Index", "TTSK");
            }
        }
        public ActionResult Details(string id)
        {

            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("Login", "Admin");
            else
            {
                var admin = from a in data.PhieuTinhTrangSucKhoes where a.MaPhieuTTSK == id select a;
                return View(admin.Single());
            }
        }
        public ActionResult Delete(string id)
        {

            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("Login", "Admin");
            else
            {
                var admin = from a in data.PhieuTinhTrangSucKhoes where a.MaPhieuTTSK == id select a;
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
                PhieuTinhTrangSucKhoe ad = data.PhieuTinhTrangSucKhoes.SingleOrDefault(n => n.MaPhieuTTSK == id);
                data.PhieuTinhTrangSucKhoes.DeleteOnSubmit(ad);
                data.SubmitChanges();
                return RedirectToAction("Index", "TTSK");
            }
        }
    }
}