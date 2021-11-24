using DACNHM.Models;
using System.Web.Mvc;

namespace DACNHM.Controllers
{
    public class HienMauController : Controller
    {
        // GET: HienMau
        dbQLHMDataContext data = new dbQLHMDataContext();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Index2()
        {
            return View();
        }
    }
}