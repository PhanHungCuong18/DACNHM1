using DACNHM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
namespace DACNHM.Controllers
{
    public class QLNDController : Controller
    {
        dbQLHMDataContext data = new dbQLHMDataContext();
        // GET: QLND
        public ActionResult Index(int? page, string keyword)
        {
            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("Login", "Admin");
            else
            {
                int pagesize = 4;
                int pagenum = (page ?? 1);
                if (!string.IsNullOrEmpty(keyword))
                {
                    TempData["kwd"] = keyword;
                    List<NguoiHienMau> dg = data.NguoiHienMaus.Where(n => n.TenNgHien.ToLower().Contains(keyword.ToLower())).ToList();
                    return View(dg.OrderByDescending(n => n.MaNgHien).ToPagedList(pagenum, pagesize));
                }
                return View(data.NguoiHienMaus.OrderByDescending(n => n.MaNgHien).ToList().ToPagedList(pagenum, pagesize));
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
                List<NguoiHienMau> dg = data.NguoiHienMaus.Where(n => n.TenNgHien.ToLower().Contains(keyword.ToLower())).ToList();
                return View("Index", dg.OrderByDescending(n => n.MaNgHien).ToPagedList(pagenum, pagesize));
            }
        }
        public ActionResult Details(int id)
        {

            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("Login", "Admin");
            else
            {
                var nguoihien = from nh in data.NguoiHienMaus where nh.MaNgHien == id select nh;
                return View(nguoihien.Single());
            }
        }
        [HttpGet]
        public ActionResult create()
        {

            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("Login", "Admin");
            else
            {
                return View();
            }
        }
        [HttpPost]
        public ActionResult create(NguoiHienMau nguoihien)
        {

            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("Login", "Admin");
            else
            {
                data.NguoiHienMaus.InsertOnSubmit(nguoihien);

                data.SubmitChanges();
                return RedirectToAction("Index", "QLND");
            }
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {

            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("Login", "Admin");
            else
            {
                var nguoihien = from nh in data.NguoiHienMaus where nh.MaNgHien == id select nh;
                return View(nguoihien.Single());
            }
        }
        [HttpPost, ActionName("Edit")]
        public ActionResult Capnhat(int id)
        {

            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("Login", "Admin");
            else
            {
                NguoiHienMau nguoihien = data.NguoiHienMaus.SingleOrDefault(n => n.MaNgHien == id);
                UpdateModel(nguoihien);
                data.SubmitChanges();
                return RedirectToAction("Index", "QLND");
            }
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {

            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("Login", "Admin");
            else
            {
                var nguoihien = from nh in data.NguoiHienMaus where nh.MaNgHien == id select nh;
                return View(nguoihien.Single());
            }
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult Xoa(int id)
        {

            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("Login", "Admin");
            else
            {
                NguoiHienMau nguoihien = data.NguoiHienMaus.SingleOrDefault(n => n.MaNgHien == id);
                data.NguoiHienMaus.DeleteOnSubmit(nguoihien);
                data.SubmitChanges();
                return RedirectToAction("Index", "QLND");
            }
        }
    }
}