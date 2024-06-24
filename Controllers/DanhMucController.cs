using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebDatMonAn.Models;
using WebDatMonAn.Repository;

namespace WebDatMonAn.Controllers
{
    public class DanhMucController : Controller
    {
        private readonly DataContext _dataContext;
        public DanhMucController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public async Task<IActionResult> Index( string Slug ="")
        {
            DanhMucModel danhmuc = _dataContext.DanhMucs.Where(c => c.SlugDanhMuc == Slug).FirstOrDefault();
            if (danhmuc == null) return RedirectToAction("Index");
            var monantheodanhmuc = _dataContext.MonAns.Include(c=>c.DanhMuc).Where(p => p.MaDanhMuc == danhmuc.MaDanhMuc);
            return View(await monantheodanhmuc.Include(x=>x.DanhMuc).OrderByDescending(p=>p.MaDanhMuc).ToListAsync());
        }
    }
}
