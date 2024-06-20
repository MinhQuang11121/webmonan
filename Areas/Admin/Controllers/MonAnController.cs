using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebDatMonAn.Models;
using WebDatMonAn.Repository;

namespace WebDatMonAn.Area.Admin.Controllers
{
    [Area("Admin")]
    public class MonAnController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly IWebHostEnvironment _webHostEnviorment;
        public MonAnController(DataContext dataContext, IWebHostEnvironment webHostEnviorment)
        {
            _dataContext = dataContext;
            _webHostEnviorment = webHostEnviorment;
        }
        public IActionResult Index()
        {
            var monan = _dataContext.MonAns.Include(c => c.DanhMuc).OrderBy(d => d.NgayTao).ToList();

            return View(monan);
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
            if (ModelState.IsValid)
            {
                monan.SlugMonAn = monan.TenMonAn.Replace(" ", "-");
                var ten = await _dataContext.MonAns.FirstOrDefaultAsync(p => p.TenMonAn == monan.TenMonAn);
                if (ten != null)
                {
                    ModelState.AddModelError("", "Món ăn đã có trong database");
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

                _dataContext.Add(monan);
                await _dataContext.SaveChangesAsync();
                TempData["success"] = "Thêm món ăn thành công";
                return RedirectToAction("Index");

            }
            

            return View(monan);
        }
    }
}
