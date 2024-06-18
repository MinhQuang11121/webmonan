using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebDatMonAn.Repository;

namespace WebDatMonAn.Area.Admin.Controllers
{
    [Area("Admin")]
    public class MonAnController : Controller
    {
        private readonly DataContext _dataContext;
        public MonAnController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public IActionResult Index()
        {
            var monan = _dataContext.MonAns.Include(c => c.DanhMuc).OrderBy(d => d.NgayTao).ToList();

            return View(monan);
        }
    }
}
