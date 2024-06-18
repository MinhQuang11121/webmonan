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
        public IActionResult Create(DanhMucModel model)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }
            return View("Create");
        } 

        
    }

    }
