using Microsoft.AspNetCore.Mvc;
using MyProWeb.Areas.Customer.Repository;
using MyProWeb.Data;

namespace MyProWeb.Areas.Customer.Components
{
    public class DanhMucViewComponent: ViewComponent
    {
        private readonly IDanhMucSPRepository _loaiSp;
        private readonly ThaimcqlGodContext _context;

        public DanhMucViewComponent(IDanhMucSPRepository danhMucSPRepository, ThaimcqlGodContext context) { 
            _loaiSp = danhMucSPRepository;
            _context = context;
        }
        public IViewComponentResult Invoke()
        {
            var list = _context.Categories.OrderBy(x => x.NameCate).ToList();
            return View(list);
        }
    }
}
