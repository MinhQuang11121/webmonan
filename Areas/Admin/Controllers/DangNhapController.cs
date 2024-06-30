using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebDatMonAn.Models.ViewModel;
using WebDatMonAn.Repository.Extension;
using WebDatMonAn.Repository;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace WebDatMonAn.Areas.Admin.Controllers
{
    [Area("Admin")]
  
    public class DangNhapController : Controller
    {

        private readonly DataContext _dataContext;
        private readonly INotyfService _notyfService;
        public DangNhapController(DataContext dataContext, INotyfService notyfService)

        {
            _notyfService = notyfService;
            _dataContext = dataContext;
        }
        [HttpGet]
        public IActionResult DangNhap(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        public async Task<IActionResult> TaiKhoan(LoginViewModel login, string returnUrl = null)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    bool isEmail = Helper.IsValidEmail(login.Email);
                    if (!isEmail)
                    {
                        _notyfService.Error("Email không hợp lệ!");
                        return PartialView(login);
                    }

                    var khachhang = _dataContext.NhanViens.AsNoTracking().SingleOrDefault(x => x.Email.Trim() == login.Email);

                    if (khachhang == null)
                    {
                        _notyfService.Error("Tài khoản không tồn tại!");
                        return PartialView(login);
                    }

                    string passs = login.MatKhau.ToMD5();

                    if (khachhang.MatKhau != passs)
                    {
                        _notyfService.Error("Sai thông tin đăng nhập!");
                        return PartialView(login);
                    }

                    HttpContext.Session.SetString("MaNV", khachhang.MaNV.ToString());
                    var taikhoanId = HttpContext.Session.GetString("MaNV");
                    var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, khachhang.TenNV),
                new Claim("MaNV", khachhang.MaNV.ToString()),
                new Claim(ClaimTypes.Role, "Admin") 
            };

                    ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "AdminScheme");
                    ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                    await HttpContext.SignInAsync("AdminScheme", claimsPrincipal);
                    _notyfService.Success("Đã đăng nhập thành công!");

                    if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "QuanTri", new { Area = "Admin" });
                    }
                }
            }
            catch (Exception ex)
            {
                _notyfService.Error("Đã xảy ra lỗi trong quá trình đăng nhập!");
                Console.WriteLine(ex.Message);
                return RedirectToAction("DangKy", "TaiKhoan");
            }

            return PartialView(login);
        }
        [HttpGet]
        public IActionResult DangXuat()
        {
            HttpContext.SignOutAsync();
            HttpContext.Session.Remove("MaNV");
            _notyfService.Error("Bạn đã đăng xuất");
            return RedirectToAction("TaiKhoan", "DangNhap");
        }





    }
}
