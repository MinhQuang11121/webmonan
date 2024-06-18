using Microsoft.AspNetCore.Mvc;
using WebDatMonAn.Models;
using WebDatMonAn.Models.ViewModel;
using WebDatMonAn.Repository;

namespace WebDatMonAn.Controllers
{
    public class GioHangController : Controller
    {
        private readonly DataContext _dataContext;
        public GioHangController (DataContext context)
        {
            _dataContext = context;
        }
        public IActionResult GioHang()
        {
            List<GioHangModel> giohang=HttpContext.Session.GetJson<List<GioHangModel>>("GioHang") ?? new List<GioHangModel>();
            GioHangViewModel gioHangVM = new()
            {
                GioHangs = giohang,
                TongTien = giohang.Sum(x => x.SoLuong * x.DonGia)
            };
            return View(gioHangVM);
        }
        public async Task<IActionResult> ThemCart(int Id, int quantity = 1)
        {
            MonAnModel monAn = await _dataContext.MonAns.FindAsync(Id);
            List<GioHangModel> giohang = HttpContext.Session.GetJson<List<GioHangModel>>("GioHang") ?? new List<GioHangModel>();
            GioHangModel GiohangItems = giohang.Where(c => c.MaMonAn == Id).FirstOrDefault();
            if (GiohangItems == null)
            {
                GioHangModel gioHangItem = new GioHangModel(monAn)
                {
                    SoLuong = quantity
                };

               
                giohang.Add(gioHangItem);

            }
            else
            {
                GiohangItems.SoLuong = quantity;
            }
            TempData["SuccessMessage"] = "Thêm giỏ hàng thành công";
            HttpContext.Session.SetJson("GioHang", giohang);
            return RedirectToAction("GioHang");
        }
        public async Task<IActionResult> Decrease(int Id)
        {
            List<GioHangModel> giohang = HttpContext.Session.GetJson<List<GioHangModel>>("GioHang") ?? new List<GioHangModel>();
            GioHangModel giohangVM = giohang.Where(c => c.MaMonAn == Id).FirstOrDefault();
            if (giohangVM.SoLuong>= 1)
            {
                --giohangVM.SoLuong;
            }
            else
            {
                giohang.RemoveAll(c => c.MaMonAn == Id);
            }
            if(giohang.Count == 0)
            {
                HttpContext.Session.Remove("GioHang");
            }
            else
            {
                HttpContext.Session.SetJson("GioHang", giohang);
            }
            return RedirectToAction("GioHang");

        }
        public async Task<IActionResult> Increase(int Id)
        {
            List<GioHangModel> giohang = HttpContext.Session.GetJson<List<GioHangModel>>("GioHang") ?? new List<GioHangModel>();
            GioHangModel giohangVM = giohang.Where(c => c.MaMonAn == Id).FirstOrDefault();
            if(giohangVM.SoLuong >= 1)
            {
                ++giohangVM.SoLuong;
            }
            else
            {
                giohang.RemoveAll(c => c.MaMonAn == Id);
            }
            if(giohang.Count == 0)
            {
                HttpContext.Session.Remove("GioHang");
            }
            else
            {
                HttpContext.Session.SetJson("GioHang", giohang);
            }


            return RedirectToAction("GioHang");
        }
        public async Task<IActionResult> Remove(int Id)
        {
            List<GioHangModel> giohang = HttpContext.Session.GetJson<List<GioHangModel>>("GioHang");
            giohang.RemoveAll(p => p.MaMonAn == Id);
            if(giohang.Count == 0)
            {
                HttpContext.Session.Remove("GioHang");
            }
            else
            {
                HttpContext.Session.SetJson("GioHang", giohang);
            }
            return RedirectToAction("GioHang");
        }
        public async Task<IActionResult>XoaHet()
        {
            HttpContext.Session.Remove("GioHang");
            return RedirectToAction("Index");
        }
    }
}
