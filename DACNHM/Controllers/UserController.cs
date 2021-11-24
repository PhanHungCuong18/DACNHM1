using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DACNHM.Models;

namespace DACNHM.Controllers
{
    public class UserController : Controller
    {

        dbQLHMDataContext data = new dbQLHMDataContext();
        // GET: User
        public ActionResult Index()
        {
            return View();
        }
        
        [HttpGet]
        public ActionResult DangKy()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Dangky(FormCollection collection, NguoiHienMau nhm)
        {
            var hoten = collection["Họ Tên"];
            var tendn = collection["Tên đăng nhập"];
            var matkhau = collection["Mật khẩu"];
            var nhaplaimatkhau = collection["Nhập lại mật khẩu"];
            var email = collection["Địa chỉ Email"];
            var diachidg = collection["Địa chỉ"];
            var dienthoaidg = collection["SDT"];
            var cccd = collection["CCCD"];
            var ngaysinh = String.Format("{0:MM/dd/yyyy}", collection["Ngày sinh"]);
            if (String.IsNullOrEmpty(hoten))
            {
                ViewData["Loi1"] = "Họ tên khách hàng không được để trống";
            }
            else if (String.IsNullOrEmpty(tendn))
            {
                ViewData["Loi2"] = "Phải nhập tên đăng nhập";
            }
            else if (String.IsNullOrEmpty(matkhau))
            {
                ViewData["Loi3"] = "Phải nhập mật khẩu";
            }
            else if (String.IsNullOrEmpty(nhaplaimatkhau))
            {
                ViewData["Loi4"] = "Phải nhập lại mật khẩu";
            }
            else if (String.IsNullOrEmpty(email))
            {
                ViewData["Loi5"] = "Email không được bỏ trống";
            }
            else if (String.IsNullOrEmpty(diachidg))
            {
                ViewData["Loi6"] = "Phải nhập địa chỉ khách hàng";
            }
            else if (String.IsNullOrEmpty(dienthoaidg))
            {
                ViewData["Loi7"] = "Phải nhập điện thoại";
            }
            else if (String.IsNullOrEmpty(dienthoaidg))
            {
                ViewData["Loi8"] = "Phải nhập CCCD";
            }
            else
            {
                nhm.TenNgHien = hoten;
                nhm.TaiKhoan = tendn;
                nhm.MatKhau = matkhau;
                nhm.Email = email;
                nhm.DiaChi = diachidg;
                nhm.Sdt = dienthoaidg;
                nhm.SoCCCD = cccd;
                nhm.NgaySinh = DateTime.Parse(ngaysinh);
                data.NguoiHienMaus.InsertOnSubmit(nhm);
                data.SubmitChanges();
                return RedirectToAction("Dangnhap");

            }
            return this.DangKy();
        }
        [HttpGet]
        public ActionResult Dangnhap()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Dangnhap(FormCollection collection)
        {

            var tendn = collection["Tên đăng nhập"];
            var matkhau = collection["Mật khẩu"];


            if (String.IsNullOrEmpty(tendn))
            {
                ViewData["Loi1"] = "Phải nhập tên đăng nhập";
            }
            else if (String.IsNullOrEmpty(matkhau))
            {
                ViewData["Loi2"] = "Phải nhập mật khẩu";
            }
            else
            {

                NguoiHienMau nhm = data.NguoiHienMaus.SingleOrDefault(n => n.TaiKhoan == tendn && n.MatKhau == matkhau);
                if (nhm != null)
                {
                    ViewBag.Thongbao = "Chúc mừng bạn đã đăng nhập thành công!";
                    
                    Session["TaiKhoan"] = nhm;
                    //ViewBag.tendn = kh.HoTen;
                    Session["id"] = nhm.MaNgHien;
                    return RedirectToAction("Index2", "HienMau");

                }
                else
                    ViewBag.Thongbao = "Tên đăng nhập hoặc mật khẩu không đúng";
            }
            return View();
        }
        [HttpGet]
        public ActionResult Quenmatkhau()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Quenmatkhau(FormCollection collection)
        {
            var tendn = collection["TenDN"];
            var email = collection["Email"];
            var dienthoaidg = collection["Dienthoai"];
            var matkhau = collection["MatKhau"];
            var nhaplaimatkhau = collection["Nhaplaimatkhau"];

            NguoiHienMau nhm = data.NguoiHienMaus.SingleOrDefault(n => n.TaiKhoan.Trim() == tendn.Trim());

            if (nhm != null)
            {
                if (nhm.Sdt.Trim() == dienthoaidg.Trim() && nhm.Email.Trim() == email.Trim())
                {
                    if (matkhau != nhaplaimatkhau)
                    {
                        ViewBag.Thongbao = "Nhập lại mật khẩu không đúng";
                    }
                    else
                    if (matkhau.Trim() == nhaplaimatkhau.Trim())
                    {
                        nhm.TaiKhoan = tendn;
                        nhm.MatKhau = matkhau;
                        nhm.Email = email;
                        nhm.Sdt = dienthoaidg;
                        data.SubmitChanges();
                        return RedirectToAction("Dangnhap", "User");
                    }
                }
                ViewBag.Thongbao = "Nhập thông tin sai vui lòng nhập lại";
            }
            else
            {
                ViewBag.Thongbao = "Nhập thông tin không đúng";
            }
            return this.Dangnhap();
        }
        public ActionResult DangXuat()
        {
            Session.Clear();
            return RedirectToAction("Index", "HienMau");
        }
        public ActionResult TTTK(int id)
        {
            var info = from IC in data.NguoiHienMaus where IC.MaNgHien == id select IC;
            return View(info.Single());
        }
        [HttpPost, ActionName("TTTK")]
        public ActionResult Sua(int id)
        {
            NguoiHienMau Cus = data.NguoiHienMaus.SingleOrDefault(n => n.MaNgHien == id);
            UpdateModel(Cus);
            data.SubmitChanges();
            return RedirectToAction("TTTK", "User");
        }
    }
}