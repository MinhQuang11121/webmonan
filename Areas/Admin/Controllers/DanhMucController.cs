using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebDatMonAn.Models;
using WebDatMonAn.Repository;

namespace WebDatMonAn.Area.Admin.Controllers
{
    [Area("Admin")]
    public class DanhMucController : Controller
    {
        private readonly DataContext _dataContext;
        public DanhMucController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var danhmuc = _dataContext.DanhMucs.OrderByDescending(c => c.MaDanhMuc).ToList();
            return View(danhmuc);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DanhMucModel danhMuc)
        {

            danhMuc.SlugDanhMuc = danhMuc.TenDanhMuc.Replace(" ", "-");
            var tendanhmuc = await _dataContext.DanhMucs.FirstOrDefaultAsync(p => p.TenDanhMuc == danhMuc.TenDanhMuc);
            if (tendanhmuc != null)
            {
                ModelState.AddModelError("", "Danh mục đã có trong database");
                return View(danhMuc);
            }
            _dataContext.Add(danhMuc);
            await _dataContext.SaveChangesAsync();
            TempData["success"] = "Thêm danh mục thành công";
            return RedirectToAction("Index");

        }
        [HttpGet]
        public async Task<IActionResult> Edit(int Id)
        {
            var danhmuc =  await _dataContext.DanhMucs.FindAsync(Id);
            return View(danhmuc);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(DanhMucModel danhmuc)
        {
            danhmuc.SlugDanhMuc = danhmuc.TenDanhMuc.Replace(" ", "-");
            var tendanhmuc = await _dataContext.DanhMucs.FirstOrDefaultAsync(p => p.TenDanhMuc == danhmuc.TenDanhMuc);
            var exists_category = _dataContext.DanhMucs.Find(danhmuc.MaDanhMuc);
            if (tendanhmuc != null)
            {
                ModelState.AddModelError("", "danh mục đã có trong database");
                return View(danhmuc);
            }
            exists_category.TenDanhMuc = danhmuc.TenDanhMuc;
            exists_category.MoTa = danhmuc.MoTa;
            exists_category.TrangThai = danhmuc.TrangThai;
            _dataContext.Update(exists_category);
            await _dataContext.SaveChangesAsync();
            return Redirect("Index");
        }
        public  async Task<IActionResult> Delete(int Id)
        {
            DanhMucModel danhMuc = await _dataContext.DanhMucs.FindAsync(Id);
            _dataContext.DanhMucs.Remove(danhMuc);
            await _dataContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }

    }

    }
