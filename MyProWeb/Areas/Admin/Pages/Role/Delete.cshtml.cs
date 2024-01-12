using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyProWeb.Data;
using System.ComponentModel.DataAnnotations;

namespace MyProWeb.Areas.Admin.Pages.Role
{
    public class DeleteModel : RolePageModel
    {
        public DeleteModel(RoleManager<IdentityRole> roleManager, AuthenDbContext context) : base(roleManager, context)
        {
        }

        
        public IdentityRole role { get; set; }
        public async Task<IActionResult> OnGet(string roleID)
        {
            if (roleID == null) return NotFound("Can't find role!");
            role = await _roleManager.FindByIdAsync(roleID);
            if (role == null)
            {
                return NotFound("Can't find role!");
            }
            return Page();

        }
        public async Task<IActionResult> OnPostAsync(string roleID)
        {
            if (roleID == null) return NotFound("Can't find role!");
            role = await _roleManager.FindByIdAsync(roleID);
            if (role==null)
            {
                return NotFound("Can't find role!");
            }
            
            var result = await _roleManager.DeleteAsync(role);
           
            if(result.Succeeded)
            {
                StatusMessage = $"Deleted successfully role name: {role.Name}";
                return RedirectToPage("./Index");
            }   
            else
            {
                result.Errors.ToList().ForEach(error =>
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                });
            }
            return Page();
        }
    }
}
