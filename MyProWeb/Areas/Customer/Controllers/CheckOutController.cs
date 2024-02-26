using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyProWeb.Data;
using MyProWeb.Helpers;
using MyProWeb.Models.Domain;
using System.Globalization;

namespace MyProWeb.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Route("customer/Myproweb/checkout/{action}")]
    public class CheckOutController : Controller
    {
        private readonly ThaimcqlGodContext _context;

        public CheckOutController(ThaimcqlGodContext context)
        {
            _context = context;
        }
        public List<CartModel> Carts
        {
            get
            {
                var data = HttpContext.Session.Get<List<CartModel>>("giohang");
                if (data == null)
                {
                    data = new List<CartModel>();
                }
                return data;
            }
        }
        public IActionResult Index()
        {
            var totalAmount = 0.0;
            if (Carts != null && Carts.Any())
            {
              
                totalAmount = Carts.Sum(item => item.DonGia * item.Quantity);
            }
           

            var formattedTotalAmount = totalAmount.ToString("C", CultureInfo.CreateSpecificCulture("vi-VN"));

            // Truyền tổng số tiền đã định dạng vào view
            ViewData["FormattedTotalAmount"] = formattedTotalAmount;
            return View();
        }
    }
}
