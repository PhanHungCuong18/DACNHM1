using DACNHM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DACNHM.Controllers
{
    public class DatLichController : Controller
    {
        dbQLHMDataContext data = new dbQLHMDataContext();
        // GET: DatLich
        public ActionResult Index()
        {
            return View();
        }
        
        
        public ActionResult Datlich(int id)
        {
            var info = from IC in data.NguoiHienMaus where IC.MaNgHien == id select IC;
            return View(info.Single());
        }
        [HttpPost, ActionName("Datlich")]
        public ActionResult Sua(int id)
        {
            NguoiHienMau Cus = data.NguoiHienMaus.SingleOrDefault(n => n.MaNgHien == id);
            UpdateModel(Cus);
            data.SubmitChanges();
            return RedirectToAction("TTTK", "User");
        }
    }
}