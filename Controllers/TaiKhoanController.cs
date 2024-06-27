using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System.Security.Claims;
using WebDatMonAn.Models;
using WebDatMonAn.Models.ViewModel;
using WebDatMonAn.Repository;
using WebDatMonAn.Repository.Extension;

namespace WebDatMonAn.Controllers
{
    public class TaiKhoanController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly INotyfService _notyfService;
        public TaiKhoanController(DataContext dataContext, INotyfService notyfService)

        {
            _notyfService = notyfService;
            _dataContext = dataContext;
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult ValidationPhone(string Phone)
        {
            try
            {
                var khachhang = _dataContext.KhachHangs.AsNoTracking().SingleOrDefault(x => x.SoDienThoai.ToLower() == Phone.ToLower());
                if (khachhang != null)

                    return Json(data: "Số điện thoại:" + Phone + "Đã được sử dụng");
                return Json(data: true);
            }
            catch
            {
                return Json(data: true);


            }


        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult ValidationEmail(string Email)
        {
            try
            {
                var khachhang = _dataContext.KhachHangs.AsNoTracking().SingleOrDefault(x => x.Email.ToLower() == Email.ToLower());
                if (khachhang != null)

                    return Json(data: "Email:" + Email + "Đã được sử dụng");
                return Json(data: true);
            }
            catch
            {
                return Json(data: true);


            }

        }
        [HttpGet]
        public IActionResult DangKy()
        {
            return PartialView();
        }
        [HttpPost]
        public async Task<IActionResult> Dangky(RegisterViewModel taikhoan)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var tenExists = await _dataContext.KhachHangs.FirstOrDefaultAsync(p => p.TenTK == taikhoan.TenTk.Trim().ToLower());

                    if (tenExists != null)
                    {

                        _notyfService.Error("Tên Tài khoản  đã tồn tại vui lòng thử lại!");
                        return PartialView(taikhoan);
                    }
                    var emailExists = await _dataContext.KhachHangs.FirstOrDefaultAsync(p => p.Email == taikhoan.Email.Trim().ToLower());

                    if (emailExists != null)
                    {

                        _notyfService.Error("Email đã tồn tại vui lòng thử lại!");
                        return PartialView(taikhoan);
                    }
                    var PhoneExists = await _dataContext.KhachHangs.FirstOrDefaultAsync(p => p.SoDienThoai == taikhoan.SoDienThoai.Trim().ToLower());

                    if (PhoneExists != null)
                    {

                        _notyfService.Error("Số điện thoại đã tồn tại vui lòng thử lại!");
                        return PartialView(taikhoan);
                    }
                    //string salt = Helper.GetRandomKey();
                    KhachHangModel khachhang = new KhachHangModel
                    {
                        TenTK = taikhoan.TenTk,
                        SoDienThoai = taikhoan.SoDienThoai.Trim().ToLower(),
                        Email = taikhoan.Email.Trim().ToLower(),
                        MatKhau = (taikhoan.MatKhau.Trim()).ToMD5(),
                        NgaySinh = taikhoan.NgaySinh,
                        TrangThai = 1,
                        Hinh = "hinh.png",
                        DiaChi = null,
                        NgayTao = DateTime.Now,
                        MaDD = 1,
                    };
                    try
                    {
                        _dataContext.Add(khachhang);
                        await _dataContext.SaveChangesAsync();
                        HttpContext.Session.SetString("MaKH", khachhang.MaKH.ToString());
                        var khachhangId = HttpContext.Session.GetString("MaKH");
                        var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.Name, khachhang.TenTK),
                            new Claim("MaKH", khachhang.MaKH.ToString())

                        };
                        ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "login");
                        ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                        await HttpContext.SignInAsync(claimsPrincipal);
                        _notyfService.Success("Đã đăng ký thành công!");

                        return RedirectToAction("Index", "Home");

                    }
                    catch
                    {
                        return RedirectToAction("DangKy", "TaiKhoan");
                    }
                }
                else
                {
                    return PartialView(taikhoan);
                }
            }
            catch
            {
                return PartialView(taikhoan);
            }
        }
        [HttpGet]

        [AllowAnonymous]
        public IActionResult Login( string returnUrl=null)
        {
            var taikhoanId = HttpContext.Session.GetString("MaKH");
            if(taikhoanId != null)
            {
                return RedirectToAction("Index", "Home");
            }
            return PartialView();
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel login, string returnUrl = null)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    bool isEmail = Helper.IsValidEmail(login.Email);
                    if (!isEmail) return PartialView(login);
                    var khachhang = _dataContext.KhachHangs.AsNoTracking().SingleOrDefault(x => x.Email.Trim() == login.Email);
                    if (khachhang == null) return RedirectToAction("DangKy");

                    // Không sử dụng salt, chỉ dùng mật khẩu trực tiếp
                    string passs = login.MatKhau.ToMD5();

                    if (khachhang.MatKhau != passs)
                    {
                        _notyfService.Error("Sai thông tin đăng nhập!");
                        return PartialView();
                    }

                    HttpContext.Session.SetString("MaKH", khachhang.MaKH.ToString());
                    var taikhoanId = HttpContext.Session.GetString("MaKH");
                    var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, khachhang.TenTK),
                new Claim("MaKH", khachhang.MaKH.ToString())
            };
                    ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "login");
                    ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                    await HttpContext.SignInAsync(claimsPrincipal);
                    _notyfService.Success("Đã đăng nhập thành công!");
                    return RedirectToAction("Index", "Home");
                }
            }
            catch
            {
                return RedirectToAction("DangKy", "TaiKhoan");
            }

            return PartialView(login);
        }

        [HttpGet]
        public IActionResult DangXuat()
        {
            HttpContext.SignOutAsync();
            HttpContext.Session.Remove("MaKH");
            _notyfService.Error("Bạn đã đăng xuất");
            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public IActionResult DoiMatKhau()
        {
            return View();
        }
        [HttpPost]
		public IActionResult DoiMatKhau(DoiMatKhau model)
		{
			try
			{
				var taikhoanId = HttpContext.Session.GetString("MaKH");
				if (taikhoanId == null)
				{
					return RedirectToAction("Login", "TaiKhoan");
				}
                if (ModelState.IsValid)
                {
					var taikhoan = _dataContext.KhachHangs.Find(Convert.ToInt32(taikhoanId));
					if (taikhoan == null) return RedirectToAction("Login", "TaiKhoan");

					var pass = model.matkhauhientai.Trim().ToMD5();
					if (pass == taikhoan.MatKhau)
					{
						string passnew = model.matkhaumoi.Trim().ToMD5();
						taikhoan.MatKhau = passnew;
						_dataContext.Update(taikhoan);
						_dataContext.SaveChanges();
						_notyfService.Success("Thay đổi mật khẩu  thành công!");
						return RedirectToAction("Login", "TaiKhoan");
					}
				}
                else
                {
                    return View();
                }
			}
			catch
			{
                _notyfService.Error("Thay đổi mật khẩu không thành công!");
				return RedirectToAction("Login", "TaiKhoan");
			}
			_notyfService.Error("Thay đổi mật khẩu không thành công!");
			return View(model);
		}

	}
}

   

