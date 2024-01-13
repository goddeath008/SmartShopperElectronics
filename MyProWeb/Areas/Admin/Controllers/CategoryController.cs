using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using MyProWeb.Data;
using MyProWeb.Models;

namespace MyProWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("admin/Proweb/Category/{action}")]
    [Authorize(Policy ="Manager")]
    public class CategoryController : Controller
    {
        private readonly ThaimcqlGodContext _context;

        public CategoryController(ThaimcqlGodContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var list = _context.Categories.ToList();
            return View(list);
        }
        public async Task<IActionResult> Search(string searchTerm)
        {
            IQueryable<Category> query = _context.Categories;
            if (!string.IsNullOrEmpty(searchTerm))
            {
                query = query.Where(p => p.NameCate.Contains(searchTerm));
            }

            List<Category> searchResults = query.ToList(); // Lấy kết quả tìm kiếm
            ViewBag.SearchTerm = searchTerm;


            return View("Index", searchResults); // Truyền kết quả tìm kiếm vào trang Index
        }
        // GET: CategoryController/Create
        public ActionResult Create()
        {
            var model = new Category();
            return View(model);
        }

        // POST: CategoryController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Category model)
        {
            if (ModelState.IsValid)
            {
                _context.Categories.Add(model);
                _context.SaveChanges();
                TempData["Success"] = "Create successfully!";
                return RedirectToAction("Index");
            }
            return View(model);
        }
        [Authorize(Policy ="ThaiMC")]
        // GET: CategoryController/Delete/5
        public ActionResult Delete(int id)
        {
            var category = _context.Categories.Find(id);

            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // POST: CategoryController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PostDelete(int IdCate)
        {
            var category = _context.Categories.Find(IdCate);

            if (category == null)
            {
                return NotFound();
            }

            // delete image in wwwroot

            _context.Categories.Remove(category);
            _context.SaveChanges();
            TempData["Success"] = "Delete Successfully!";
            return RedirectToAction("Index");
        }
        [Authorize(Policy ="Editor")]
        public ActionResult Edit(int id)
        {
            var category = _context.Categories.Find(id);

            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // POST: CategoryController/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(int Idcate, Category updatedCategory)
        {
            if (Idcate != updatedCategory.Idcate)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var existingCategory = _context.Categories.Find(Idcate);

                if (existingCategory == null)
                {
                    return NotFound();
                }

                existingCategory.NameCate = updatedCategory.NameCate;
                existingCategory.ImgCate = updatedCategory.ImgCate;

                _context.Update(existingCategory);
                _context.SaveChanges();
                TempData["Success"] = "Update Successfully!";
                return RedirectToAction("Index");
            }

            return View(updatedCategory);
        }
    }
}
