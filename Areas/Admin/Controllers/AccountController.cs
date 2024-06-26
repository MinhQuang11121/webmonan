using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebDatMonAn.Repository;

namespace WebDatMonAn.Areas.Admin.Controllers
{

    [Area("Admin")]
    public class AccountController : Controller
    {
        private readonly DataContext _dataContext;
        public AccountController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            ViewData["QuyenTruyCap"] = new SelectList(_dataContext.PhanQuyens, "MaCN", "TenQuyen");
            var account = _dataContext.CTCNs.Include(c => c.NhanVien).Include(c => c.ChucNang);

            return View(await account.ToListAsync());
        }
        [HttpGet]
        public IActionResult Create()
        {
            ViewData["QuyenTruyCap"] = new SelectList(_dataContext.PhanQuyens, "MaCN", "MoTa");
            return View();
        }
    }
}
