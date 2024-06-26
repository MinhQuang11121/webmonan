using Microsoft.AspNetCore.Mvc;
using WebDatMonAn.Repository;

namespace WebDatMonAn.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ShipperController : Controller
    {
        private readonly DataContext _dataContext;

        public ShipperController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var shipper = _dataContext.Shippers.ToList();
            return View(shipper);
        }
    }
}
