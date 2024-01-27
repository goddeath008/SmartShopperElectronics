using Microsoft.AspNetCore.Mvc;
using MyProWeb.Data;
using MyProWeb.Helpers;
using MyProWeb.Models.Domain;

namespace MyProWeb.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Route("customer/Myproweb/cart/{action}")]
    public class CartController : Controller
    {
        private readonly ThaimcqlGodContext _context;

        public CartController(ThaimcqlGodContext context)
        {
            _context = context;
        }
        public List<CartModel> Carts
        {
            get
            {
                var data = HttpContext.Session.Get<List<CartModel>>("giohang");
                if (data ==null)
                {
                    data = new List<CartModel>();
                }
                return data;
            }
        }
        public IActionResult Index()
        {
            if (Carts != null && Carts.Any())
            {
                ViewData["CartCount"] = Carts.Count();
            }
            else
            {
                ViewData["CartCount"] = 0;
            }

            return View(Carts);
        }

        public IActionResult AddToCart(int id, int SoLuong, string type="Normal")
        {
            var myCart = Carts;
            var item = myCart.SingleOrDefault(p => p.IdProduct == id);
            if(item == null)
            {
                var hanghoa = _context.Products.SingleOrDefault(p => p.Idpro == id);
                item = new CartModel() { 
                    IdProduct=id,
                    ProductName=hanghoa.NamePro,
                    DonGia = hanghoa.Price.Value,
                    Quantity=SoLuong,
                    Image = hanghoa.ImgPro

                };
                myCart.Add(item);
            }
            else
            {
                item.Quantity +=SoLuong;
            }
            HttpContext.Session.Set("giohang", myCart);
            if(type == "ajax")
            {
                return Json(new
                {
                    SoLuong = Carts.Count()
                });
            }
            return RedirectToAction("Index");
        }
       
        public IActionResult RemoveFromCart(int id, string type)
        {
            var myCart = Carts;
            var item = myCart.SingleOrDefault(p => p.IdProduct == id);
            if (type == "ajax")
            {
               
                if (item != null)
                {
                    myCart.Remove(item);
                    
                }
                HttpContext.Session.Set("giohang", myCart);
                return Json(new { success = true });
            }
            else
            {
                
                // Xử lý cho các loại yêu cầu khác
                return RedirectToAction("Index"); // Hoặc chuyển hướng đến trang khác
            }
        }
    }
}
