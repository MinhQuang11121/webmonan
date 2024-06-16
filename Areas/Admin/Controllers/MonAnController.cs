using Microsoft.AspNetCore.Mvc;

namespace WebDatMonAn.Area.Admin.Controllers
{
    public class MonAnController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
