using Microsoft.AspNetCore.Mvc;

namespace WebDatMonAn.Controllers
{
    public class DanhMucController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
