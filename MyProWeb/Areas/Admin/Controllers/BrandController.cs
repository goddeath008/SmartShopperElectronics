using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using MyProWeb.Data;
using MyProWeb.Models;

namespace MyProWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("admin/Proweb/brand/{action}")]
    public class BrandController : Controller
    {
        private readonly ThaimcqlGodContext _context;

        public BrandController(ThaimcqlGodContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var list = _context.Brands.ToList();
            return View(list);
        }
        public async Task<IActionResult> Search(string searchTerm)
        {
            IQueryable<Brand> query = _context.Brands;
            if (!string.IsNullOrEmpty(searchTerm))
            {
                query = query.Where(p => p.NameBrand.Contains(searchTerm));
            }

            List<Brand> searchResults =  query.ToList(); // Lấy kết quả tìm kiếm
            ViewBag.SearchTerm = searchTerm;


            return View("Index", searchResults); // Truyền kết quả tìm kiếm vào trang Index
        }

        // GET: BrandController/Create
        public ActionResult Create()
        {
            var model = new Brand();
            return View(model);
        }

        // POST: BrandController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Brand model)
        {


            if (ModelState.IsValid)
            {
                _context.Brands.Add(model);
                _context.SaveChanges();
                TempData["Success"] = "Create succesfully!";
                return RedirectToAction("Index");
            }
            return View(model);
        }
        // GET: BrandController/Delete/5
        public ActionResult Delete(int id)
        {
            var brand = _context.Brands.Find(id);

            if (brand == null)
            {
                return NotFound();
            }

            return View(brand);
        }

        // POST: BrandController/Delete/5
        [HttpPost()]
        [ValidateAntiForgeryToken]
        public ActionResult PostDelete(int IdBrand)
        {
            var brand = _context.Brands.Find(IdBrand);

            if (brand == null)
            {
                return NotFound();
            }

            //delete image in wwroot
           

            _context.Brands.Remove(brand);
            _context.SaveChanges();
            TempData["Success"] = "Delete Succesfully!";
            return RedirectToAction("Index");
        }
        public ActionResult Edit(int id)
        {
            var brand = _context.Brands.Find(id);

            if (brand == null)
            {
                return NotFound();
            }

            return View(brand);
        }

        // POST: BrandController/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(int IdBrand, Brand updatedBrand)
        {
            if (IdBrand != updatedBrand.Idbrand)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var existingBrand = _context.Brands.Find(IdBrand);
                if (existingBrand == null)
                {
                    return NotFound();
                }

               
                existingBrand.NameBrand = updatedBrand.NameBrand;
                existingBrand.ImgBrand = updatedBrand.ImgBrand;

                _context.Update(existingBrand);
                _context.SaveChanges();
                TempData["Success"] = "Update Successfully!";
                return RedirectToAction("Index");

            }

            return View(updatedBrand);
        }


    }
}
