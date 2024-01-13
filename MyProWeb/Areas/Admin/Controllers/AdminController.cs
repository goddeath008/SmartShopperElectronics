using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MyProWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("admin/Proweb/trang-chu/{action}")]
    [Authorize(Policy ="Manager")]
    public class AdminController : Controller
    {
        
        public IActionResult Index()
        {
            return View();
        }
    }
}
