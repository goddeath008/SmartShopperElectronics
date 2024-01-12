using Microsoft.AspNetCore.Mvc;
using MyProWeb.Data;
using System.ComponentModel;

namespace MyProWeb.Components
{
    public class BrandViewComponent : ViewComponent
    {
        private readonly ThaimcqlGodContext _context;

        public BrandViewComponent(ThaimcqlGodContext context)
        {
            _context = context;
        }
        public IViewComponentResult Invoke()
        {
            var list = _context.Brands.ToList();
            return View(list);
        }
    }
}
