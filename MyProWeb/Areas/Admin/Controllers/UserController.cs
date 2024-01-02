using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using MyProWeb.Data;
using MyProWeb.Models;

namespace MyProWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("admin/Proweb/User/{action}")]
    public class UserController : Controller
    {
        private readonly ThaimcqlGodContext _context;

        public UserController(ThaimcqlGodContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var list = _context.Users.ToList();
            return View(list);
        }

        // GET: UserController/Create
        public ActionResult Create()
        {
            var model = new User();
            return View(model);
        }

        // POST: UserController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(User model)
        {
            if (ModelState.IsValid)
            {
                _context.Users.Add(model);
                _context.SaveChanges();
                TempData["Success"] = "Create successfully!";
                return RedirectToAction("Index");
            }
            return View(model);
        }

        // GET: UserController/Delete/5
        public ActionResult Delete(int id)
        {
            var user = _context.Users.Find(id);

            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: UserController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PostDelete(int Iduser)
        {
            var user = _context.Users.Find(Iduser);

            if (user == null)
            {
                return NotFound();
            }

            // delete image in wwwroot

            _context.Users.Remove(user);
            _context.SaveChanges();
            TempData["Success"] = "Delete Successfully!";
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            var user = _context.Users.Find(id);

            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: UserController/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(int Iduser, User updatedUser)
        {
            if (Iduser != updatedUser.Iduser)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var existingUser = _context.Users.Find(Iduser);

                if (existingUser == null)
                {
                    return NotFound();
                }
                existingUser.PhoneNumber = updatedUser.PhoneNumber;
                existingUser.NameUser = updatedUser.NameUser;
                existingUser.PassUser = updatedUser.PassUser;

                _context.Update(existingUser);
                _context.SaveChanges();
                TempData["Success"] = "Update Successfully!";
                return RedirectToAction("Index");
            }

            return View(updatedUser);
        }
    }
}
