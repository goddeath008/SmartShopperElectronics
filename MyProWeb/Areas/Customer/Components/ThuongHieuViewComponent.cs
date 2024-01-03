using Microsoft.AspNetCore.Mvc;
using MyProWeb.Data;
using SQLitePCL;

namespace MyProWeb.Areas.Customer.Components
{
    public class ThuongHieuViewComponent: ViewComponent
    {
        private readonly ThaimcqlGodContext _context;

        public ThuongHieuViewComponent(ThaimcqlGodContext context) 
        {
            _context = context;
        }
        public IViewComponentResult Invoke()
        {
            var list = _context.Brands.OrderBy(b=> b.NameBrand).ToList();
            return View(list);
        }
    }
}
