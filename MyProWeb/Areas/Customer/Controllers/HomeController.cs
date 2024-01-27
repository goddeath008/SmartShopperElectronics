using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyProWeb.Data;
using MyProWeb.Models.Domain;
using System.Diagnostics;

namespace MyProWeb.Areas.Customer.Controllers
{
    [Area("Customer")]    

    public class HomeController : Controller
    {
        

        private readonly ThaimcqlGodContext _context;

        public HomeController(ThaimcqlGodContext context)
        {

            _context = context;
        }
        public async Task<IActionResult>  Index()
        {
            
            var list = _context.Products
                .Include(a=>a.IdbrandNavigation)
                .Include(a=>a.IdcateNavigation)
                .ToList();
            return View(list);
        }
        public IActionResult test()
        {
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}