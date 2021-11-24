using DACNHM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
namespace DACNHM.Controllers
{
    public class TKADController : Controller
    {
        dbQLHMDataContext data = new dbQLHMDataContext();
        // GET: TKAD
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
                    List<Admin> ad = data.Admins.Where(n => n.Hoten.ToLower().Contains(keyword.ToLower())).ToList();
                    return View(ad.OrderByDescending(n => n.UserAdmin).ToPagedList(pagenum, pagesize));
                }
                return View(data.Admins.OrderByDescending(n => n.UserAdmin).ToList().ToPagedList(pagenum, pagesize));
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
                int pagesize = 3;
                int pagenum = 1;

                TempData["kwd"] = keyword;
                List<Admin> ad = data.Admins.Where(n => n.Hoten.ToLower().Contains(keyword.ToLower())).ToList();
                return View("Index", ad.OrderByDescending(n => n.UserAdmin).ToPagedList(pagenum, pagesize));
            }
        }
        public ActionResult Details(string id)
        {

            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("Login", "Admin");
            else
            {
                var admin = from a in data.Admins where a.UserAdmin == id select a;
                return View(admin.Single());
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
        public ActionResult create(Admin ad)
        {

            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("Login", "Admin");
            else
            {
                data.Admins.InsertOnSubmit(ad);

                data.SubmitChanges();
                return RedirectToAction("Index", "TKAD");
            }
        }
        [HttpGet]
        public ActionResult Edit(String id)
        {

            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("Login", "Admin");
            else
            {
                var admin = from a in data.Admins where a.UserAdmin == id select a;
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
                Admin ad = data.Admins.SingleOrDefault(n => n.UserAdmin == id);
                UpdateModel(ad);
                data.SubmitChanges();
                return RedirectToAction("Index", "TKAD");
            }
        }
        [HttpGet]
        public ActionResult Delete(string id)
        {

            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("Login", "Admin");
            else
            {
                var admin = from a in data.Admins where a.UserAdmin == id select a;
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
                Admin ad = data.Admins.SingleOrDefault(n => n.UserAdmin == id);
                data.Admins.DeleteOnSubmit(ad);
                data.SubmitChanges();
                return RedirectToAction("Index", "TKAD");
            }
        }
    }
}