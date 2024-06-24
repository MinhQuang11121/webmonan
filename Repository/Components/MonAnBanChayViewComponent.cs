using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebDatMonAn.Repository.Components
{
	public class MonAnBanChayViewComponent:ViewComponent

	{
		private readonly DataContext _dataContext;
		public MonAnBanChayViewComponent(DataContext dataContext)
		{
			_dataContext = dataContext;
		}
		public async Task<IViewComponentResult> InvokeAsync()
		{
			var monan = await _dataContext.MonAns.Include(c => c.DanhMuc).OrderByDescending(x => x.NgayTao).Skip(1).Take(3).ToListAsync();
			return View(monan);
		}
	}
}
