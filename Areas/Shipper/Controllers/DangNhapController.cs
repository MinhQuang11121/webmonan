using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebDatMonAn.Models.ViewModel;
using WebDatMonAn.Repository.Extension;
using WebDatMonAn.Repository;
using AspNetCoreHero.ToastNotification.Abstractions;

namespace WebDatMonAn.Areas.Shipper.Controllers
{
    [Area("Shipper")]
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
        public IActionResult Login(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return PartialView();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel login, string returnUrl = null)
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

                    var shipper = _dataContext.Shippers.SingleOrDefault(x => x.Email.Trim() == login.Email);

                    if (shipper == null)
                    {
                        _notyfService.Error("Tài khoản không tồn tại!");
                        return PartialView(login);
                    }

                    // Assuming MatKhau is stored as a hash in the database
                    string hashedPassword = login.MatKhau.ToMD5();
                    if (shipper.MatKhau != hashedPassword)
                    {
                        _notyfService.Error("Sai thông tin đăng nhập!");
                        return PartialView(login);
                    }

                    // Set session if needed
                    HttpContext.Session.SetString("MaShip", shipper.MaShip.ToString());

                    // Set claims and sign in
                    var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, shipper.Email), // Consider using email or username here
                new Claim("MaShip", shipper.MaShip.ToString()),
                new Claim(ClaimTypes.Role, "Shipper") // Use correct role here
            };

                    var claimsIdentity = new ClaimsIdentity(claims, "ShipperScheme");
                    var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                    await HttpContext.SignInAsync("ShipperScheme", claimsPrincipal);

                    _notyfService.Success("Đã đăng nhập thành công!");

                    if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home"); 
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
            HttpContext.Session.Remove("MaShip");
            _notyfService.Error("Bạn đã đăng xuất");
            return RedirectToAction("Login", "DangNhap");
        }

    }
}
