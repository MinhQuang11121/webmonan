using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebDatMonAn.Repository;

namespace WebDatMonAn.Areas.Shipper.Controllers
{
    [Area("Shipper")]
    public class HomeController : Controller
    {
        private readonly DataContext _dataContext;
        public HomeController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public IActionResult Index()
        {
            var hoaDons = _dataContext.HoaDons.Include(h => h.KhachHang).OrderByDescending(h => h.NgayDat).ToList();

            var orderDetails = _dataContext.CTHDs
                                .Include(ct => ct.MonAn)
                                .Include(ct => ct.HoaDon)
                                .Where(ct => hoaDons.Select(h => h.MaHD).Contains(ct.MaHD))
                                .ToList();

            ViewBag.HoaDons = hoaDons;
            ViewBag.OrderDetails = orderDetails;

            return View();
        }




    }
}
