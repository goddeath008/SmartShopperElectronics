using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyProWeb.Data;


namespace MyProWeb.Areas.Admin.Pages.Role
{
    [Authorize(Policy = "ThaiMC")]
    public class IndexModel : RolePageModel
    {
        public IndexModel(RoleManager<IdentityRole> roleManager, AuthenDbContext context) : base(roleManager, context)
        {
        }

        public List<IdentityRole> roles { get; set; }
        public async Task OnGet()
        {
            roles = await _roleManager.Roles.OrderBy(a=>a.Name).ToListAsync();
        }
        public void OnPost() => RedirectToPage();
    }
}
