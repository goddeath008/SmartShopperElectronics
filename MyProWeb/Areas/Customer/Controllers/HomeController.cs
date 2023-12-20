using Microsoft.AspNetCore.Mvc;
using MyProWeb.Data;
using MyProWeb.Models;
using MyProWeb.Models.Domain;
using System.Diagnostics;

namespace MyProWeb.Areas.Guests.Controllers
{
    [Area("Customer")]    

    public class HomeController : Controller
    {
        

        private readonly ThaimcqlGodContext _context;

        public HomeController(ThaimcqlGodContext context)
        {

            _context = context;
        }
        public IActionResult Index()
        {
            var list = _context.Brands.ToList();
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