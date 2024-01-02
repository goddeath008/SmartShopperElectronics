using Microsoft.AspNetCore.Mvc;
using MyProWeb.Data;

namespace MyProWeb.Areas.Customer.Components
{
    public class CategoryViewComponent : ViewComponent
    {
        private readonly ThaimcqlGodContext _context;

        public CategoryViewComponent(ThaimcqlGodContext context)
        {
            _context = context;
        }
        public IViewComponentResult Invoke()
        {
            var list = _context.Categories.ToList();
            return View(list);
        }
    }
}
