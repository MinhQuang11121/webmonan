using Microsoft.AspNetCore.Mvc;

namespace WebDatMonAn.Area.Admin.Controllers
{
    public class DanhMucController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
