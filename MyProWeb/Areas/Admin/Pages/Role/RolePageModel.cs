using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyProWeb.Data;
using SQLitePCL;

namespace MyProWeb.Areas.Admin.Pages.Role
{
    public class RolePageModel:PageModel
    {
        protected readonly RoleManager<IdentityRole> _roleManager;
        protected readonly AuthenDbContext _context;

        [TempData]
        public string StatusMessage {  get; set; }

        public RolePageModel(RoleManager<IdentityRole> roleManager, AuthenDbContext context) 
        { 
            _roleManager = roleManager;
            _context = context;
        }
    }
}
