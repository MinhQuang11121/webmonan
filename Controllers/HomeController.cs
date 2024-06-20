using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using WebDatMonAn.Models;
using WebDatMonAn.Repository;

namespace WebDatMonAn.Controllers
{
    public class HomeController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, DataContext context)
        {
            _dataContext = context;
            _logger = logger;
        }

        public IActionResult Index()

        {
          
            var monan = _dataContext.MonAns.Include(c => c.DanhMuc).AsNoTracking().Where(d=>d.TrangThai==1).OrderBy(x => x.NgayTao).Take(8).ToList();
            return View(monan);
        }
        public IActionResult TimKiem(string? query)
        {
            var monans = _dataContext.MonAns.Include(c => c.DanhMuc).AsQueryable();
            if(query != null)
            {
                monans = monans.Where(p => p.TenMonAn.Contains(query)
                || p.DonGia.ToString().Contains(query) ||
                p.DanhMuc.TenDanhMuc.Contains(query));
            }
            return View(monans.ToList());
        }
         
        public async  Task<IActionResult> Detail( int Id)
        {
            if (Id == null) return RedirectToAction("Index");
            var monan =   _dataContext.MonAns.Include(x => x.DanhMuc).Where(p => p.MaMonAn == Id).FirstOrDefault();

            return View(monan);
        }
        public IActionResult Tatcamonan()
        {
            var monan = _dataContext.MonAns.Include(c => c.DanhMuc).AsNoTracking().OrderBy(x => x.NgayTao).Where(d => d.TrangThai == 1).ToList();
            return View(monan);

           
        }
        public async Task<IActionResult> Giacao()
        {
            var monAns = await _dataContext.MonAns
                                            .Where(c => c.TrangThai == 1)
                                           .OrderBy(m => m.DonGia) 
                                           .ToListAsync();
            return View("Tatcamonan", monAns);
        }
        public async Task<IActionResult> Giathap()
        {
            var monAns = await _dataContext.MonAns
                                            .Where(c=>c.TrangThai ==1).
                                             OrderByDescending(m => m.DonGia)
                                           .ToListAsync();
            return View("Tatcamonan", monAns);
        }
        public async Task<IActionResult> Gia50K()
        {
            var monAns = await _dataContext.MonAns
                                       .Where(m => m.DonGia <= 50000)
                                       .ToListAsync();
            return View("Tatcamonan", monAns);
        }
        public async Task<IActionResult> Gia50KDen100k()
        {
            var monAns = await _dataContext.MonAns
                                       .Where(m => m.DonGia <= 100000 && m.DonGia >=50000)
                                       .ToListAsync();
            return View("Tatcamonan", monAns);
        }
        public async Task<IActionResult> Tren100K()
        {
            var monAns = await _dataContext.MonAns
                                       .Where(m => m.DonGia >= 100000)
                                       .ToListAsync();
            return View("Tatcamonan", monAns);
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
