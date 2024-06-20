using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebDatMonAn.Models;
using WebDatMonAn.Models.ViewModel;
using WebDatMonAn.Repository;

namespace WebDatMonAn.Controllers
{
    public class TaiKhoanController : Controller
    {
        private readonly DataContext _dataContext;
     public TaiKhoanController( DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        [HttpGet]
        public IActionResult DangKy()
        {
            return PartialView();
        }
      
      

        public IActionResult Login()
        {
            return PartialView();
        }
    }
}

   

