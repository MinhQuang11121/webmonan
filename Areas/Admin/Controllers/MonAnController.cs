using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebDatMonAn.Models;
using WebDatMonAn.Repository;
using X.PagedList;

namespace WebDatMonAn.Area.Admin.Controllers
{
    [Area("Admin")]
    public class MonAnController : Controller
    {
        private readonly DataContext _dataContext;
		private readonly INotyfService _notyfService;
		private readonly IWebHostEnvironment _webHostEnviorment;
        public MonAnController(DataContext dataContext, IWebHostEnvironment webHostEnviorment, INotyfService notyfService)
        {
            _dataContext = dataContext;
            _webHostEnviorment = webHostEnviorment;
            _notyfService = notyfService;
        }
        public IActionResult Index(int? page)
        {
            int pageSize = 4;
            int pagenumber = page == null || page < 0 ? 1 : page.Value;
            var dsmonan = _dataContext.MonAns.AsNoTracking().Include(c => c.DanhMuc).OrderBy(d => d.NgayTao);
            PagedList<MonAnModel> models = new PagedList<MonAnModel>(dsmonan, pagenumber, pageSize);
            ViewBag.CurrentPage = pagenumber;

            return View(models);
           
        }
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.DanhMucs = new SelectList(_dataContext.DanhMucs, "MaDanhMuc", "TenDanhMuc");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MonAnModel monan)
        {
            ViewBag.DanhMucs = new SelectList(_dataContext.DanhMucs, "MaDanhMuc", "TenDanhMuc", monan.MaDanhMuc);
          
                monan.SlugMonAn = monan.TenMonAn.Replace(" ", "-");
                var ten = await _dataContext.MonAns.FirstOrDefaultAsync(p => p.TenMonAn == monan.TenMonAn);
                if (ten != null)
                {
                _notyfService.Error("Món ăn đã có trong database");
                    return View();
                }

                if (monan.ImageUpload != null)
                {
                    string uploadDir = Path.Combine(_webHostEnviorment.WebRootPath, "image/monan");
            
                    string imagename = Guid.NewGuid().ToString() + "_" + monan.ImageUpload.FileName;
                    string filePath = Path.Combine(uploadDir, imagename);

                    FileStream fs = new FileStream(filePath, FileMode.Create);
                    await monan.ImageUpload.CopyToAsync(fs);
                    fs.Close();
                    monan.HinhAnh = imagename;

                }
                     monan.NgayTao = DateTime.Now;
           

                _dataContext.Add(monan);
                await _dataContext.SaveChangesAsync();
                TempData["success"] = "Thêm món ăn thành công";
                return RedirectToAction("Index");

          
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int Id)
        {
            MonAnModel monAn = await _dataContext.MonAns.FindAsync(Id);
            ViewBag.DanhMucs = new SelectList(_dataContext.DanhMucs, "MaDanhMuc", "TenDanhMuc", monAn.MaDanhMuc);
            return View(monAn);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(MonAnModel monan)
        {
            ViewBag.DanhMucs = new SelectList(_dataContext.DanhMucs, "MaDanhMuc", "TenDanhMuc", monan.MaDanhMuc);
           
            var exists_product =  _dataContext.MonAns.Find(monan.MaMonAn);
			var tenmonan = await _dataContext.MonAns.FirstOrDefaultAsync(p => p.TenMonAn == monan.TenMonAn);

			if (exists_product != null)
            {
               
                return View(monan);
            }

            monan.SlugMonAn = monan.TenMonAn.Replace(" ", "-");
        

            if (tenmonan != null)
            {
                ModelState.AddModelError("", "Món ăn đã có trong database");
                return View(monan);
            }

            if (monan.ImageUpload != null)
            {
                string uploadDir = Path.Combine(_webHostEnviorment.WebRootPath, "image/monan");
                string imagename = Guid.NewGuid().ToString() + "_" + monan.ImageUpload.FileName;
                string filePath = Path.Combine(uploadDir, imagename);

                using (var fs = new FileStream(filePath, FileMode.Create))
                {
                    await monan.ImageUpload.CopyToAsync(fs);
                }
                exists_product.HinhAnh = imagename;
            }

            exists_product.TenMonAn = monan.TenMonAn;
            exists_product.MoTa = monan.MoTa;
            exists_product.DiaChiQuan = monan.DiaChiQuan;
            exists_product.SoLuong = monan.SoLuong;
            exists_product.DonGia = monan.DonGia;
            exists_product.Video = monan.Video;
            exists_product.MaDanhMuc = monan.MaDanhMuc;
            exists_product.NgayTao = monan.NgayTao;

            _dataContext.Update(exists_product);
            await _dataContext.SaveChangesAsync();
            TempData["success"] = "Cập nhật món ăn thành công";
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int Id)
        {
            MonAnModel monan = await _dataContext.MonAns.FindAsync(Id);
            if (!string.Equals(monan.HinhAnh, "noname.jpg"))
            {
                string uploadDir = Path.Combine(_webHostEnviorment.WebRootPath, "image/monan");
                string filePath = Path.Combine(uploadDir, monan.HinhAnh);
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }
            }
            _dataContext.MonAns.Remove(monan);
            await _dataContext.SaveChangesAsync();
            TempData["error"] = "Món ăn đã xóa";
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> ChiTiet(int Id)
        {
            var monAn = await _dataContext.MonAns.Include(x => x.DanhMuc).FirstOrDefaultAsync(x => x.MaMonAn == Id);
            ViewBag.DanhMucs = new SelectList(_dataContext.DanhMucs, "MaDanhMuc", "TenDanhMuc", monAn.MaDanhMuc);
            return View(monAn);
        }
    }
}
