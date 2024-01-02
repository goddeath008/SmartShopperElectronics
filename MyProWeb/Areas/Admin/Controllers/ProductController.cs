using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Microsoft.EntityFrameworkCore;
using MyProWeb.Data;
using MyProWeb.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using EFCoreExtensions = Microsoft.EntityFrameworkCore.EntityFrameworkQueryableExtensions;



namespace MyProWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("admin/Proweb/product/{action}")]
    public class ProductController : Controller
    {
        private readonly ThaimcqlGodContext _context;

       public ProductController(ThaimcqlGodContext context)
        {
            _context = context;
        }
        // GET: ProductController
        public ActionResult Index()
        {
            var a = _context.Products
               .Include(a=>a.IdbrandNavigation)
               .Include(a=>a.IdcateNavigation)
                .ToList();
            ViewBag.Brands =  _context.Brands.ToList();
            return View(a);
        }
        public async Task<IActionResult> Search(string searchTerm, int? brandId)
        {
            IQueryable<Product> query = _context.Products;
            if (!string.IsNullOrEmpty(searchTerm))
            {
                query = query.Where(p => p.NamePro.Contains(searchTerm));
            }

            if (brandId.HasValue)
            {
                query = query.Where(p => p.Idbrand == brandId);
            }

            var products = await query
                .Include(p => p.IdbrandNavigation)
                .Include(p => p.IdcateNavigation)                
                .ToListAsync();

           

            return View("Index", products);
        }

        // GET: ProductController/Details/5
        [Route("admin/Proweb/product/Details")]
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = _context.Products
                .Include(a=>a.IdbrandNavigation)
                .Include(a=>a.IdcateNavigation)
                .FirstOrDefault(m => m.Idpro == id);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }


        // GET: ProductController/Create
        // GET: Products/Create
        public IActionResult Create()
        {
            // Kiểm tra xem các bảng `Brands`, `Categories`, và `Users` có chứa dữ liệu hay không.
            if (_context.Brands.Count() == 0)
            {
                TempData["Error"] = "Thêm dữ liệu vào bảng Brands trước khi tạo sản phẩm.";
                return View();
            }
            if (_context.Categories.Count() == 0)
            {
                TempData["Error"] = "Thêm dữ liệu vào bảng Categories trước khi tạo sản phẩm.";
                return View();
            }
            if (_context.Users.Count() == 0)
            {
                TempData["Error"] = "Thêm dữ liệu vào bảng Users trước khi tạo sản phẩm.";
                return View();
            }

            // Điền các danh sách vào đối tượng ViewBag
            ViewBag.BrandId = new SelectList(_context.Brands, "Idbrand", "NameBrand");
            ViewBag.CategoryId = new SelectList(_context.Categories, "Idcate", "NameCate");
            ViewBag.UserId = new SelectList(_context.Users, "Iduser", "UserName");

            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Product product)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ViewBag.BrandId = new SelectList(_context.Brands, "Idbrand", "NameBrand", product.Idbrand);
                    ViewBag.CategoryId = new SelectList(_context.Categories, "Idcate", "NameCate", product.Idcate);
                    ViewBag.UserId = new SelectList(_context.Users, "Iduser", "UserName", product.Iduser);

                    _context.Products.Add(product);
                    _context.SaveChanges();

                    TempData["Success"] = "Create Successfully";
                    return RedirectToAction("Index");
                }

                // If ModelState is not valid, return to the Create view with validation errors
                return View(product);
            }
            catch (Exception ex)
            {
                // Log the exception for debugging and troubleshooting
                TempData["Error"] = "An error occurred while creating the product.";

                // If an error occurs, return to the Create view with an error message
                ViewBag.BrandId = new SelectList(_context.Brands, "Idbrand", "NameBrand");
                ViewBag.CategoryId = new SelectList(_context.Categories, "Idcate", "NameCate");
                ViewBag.UserId = new SelectList(_context.Users, "Iduser", "UserName");
                return View(product);
            }
        }

        // GET: ProductController/Edit/5

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product =  _context.Products.FirstOrDefault(x => x.Idpro == id);


            if (product == null)
            {
                return NotFound();
            }

            ViewData["BrandId"] = new SelectList(_context.Brands, "IdBrand", "NameBrand", product.Idbrand);
            ViewData["CategoryId"] = new SelectList(_context.Categories, "IdCate", "NameCate", product.Idcate);

            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Product product)
        {
            if (id != product.Idpro)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                     _context.SaveChanges();

                    TempData["Success"] = "Update Successfully!";
                    return RedirectToAction("Index");
                }
                catch (DbUpdateConcurrencyException)
                {
                    // Handle concurrency exception if necessary
                    // For example, you may want to reload the entity and apply changes again.
                    TempData["Error"] = "Concurrency error!";
                    return RedirectToAction("Index");
                }
            }

            ViewData["BrandId"] = new SelectList(_context.Brands, "IdBrand", "NameBrand", product.Idbrand);
            ViewData["CategoryId"] = new SelectList(_context.Categories, "IdCate", "NameCate", product.Idcate);
            TempData["Error"] = "Wrong!";
            return View(product);
        }


        // GET: ProductController/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product =  _context.Products
               
                
                .FirstOrDefault(m => m.Idpro == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: BrandController/Delete/5
        // POST: Products/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var product = await _context.Products.FindAsync(id);

                if (product == null)
                {
                    TempData["Error"] = "Product not found.";
                    return NotFound();
                }

                _context.Products.Remove(product);
                await _context.SaveChangesAsync();

                TempData["Success"] = "Product deleted successfully.";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                // Log the exception for debugging and troubleshooting
               
                TempData["Error"] = "An error occurred while deleting the product.";
                return View("Wrong!"); // Return to the Delete view with error message
            }
        }
    }
}
