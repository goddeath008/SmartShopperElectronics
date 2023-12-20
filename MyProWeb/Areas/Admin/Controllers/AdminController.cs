using Microsoft.AspNetCore.Mvc;

namespace MyProWeb.Areas.Admin.Controllers
{
    public class AdminController : Controller
    {
        [Area("Admin")]
        [Route("admin/Proweb/trang-chu/{action}")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
