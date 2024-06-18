using Microsoft.AspNetCore.Mvc;

namespace WebDatMonAn.Controllers
{
    public class TaiKhoanController : Controller
    {
        public IActionResult DangKy()
        {
            return PartialView();
        }
        public IActionResult Login()
        {
            return PartialView();
        }
    }
}
