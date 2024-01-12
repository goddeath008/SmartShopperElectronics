using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyProWeb.Data;
using System.ComponentModel.DataAnnotations;

namespace MyProWeb.Areas.Admin.Pages.Role
{
    public class EditModel : RolePageModel
    {
        public EditModel(RoleManager<IdentityRole> roleManager, AuthenDbContext context) : base(roleManager, context)
        {
        }

        public class InputModel
        {
            [Display(Name ="Name-Role")]
            [Required(ErrorMessage ="Invalid {0}!")]
            [StringLength(256,MinimumLength =2, ErrorMessage ="{0} have to length from {2} to {1} characters!")]
            public string Name { get; set; }
        }
        [BindProperty]
        public InputModel Input { get; set; }
        public IdentityRole role { get; set; }
        public async Task<IActionResult> OnGet(string roleID)
        {
            if (roleID == null) return NotFound("Can't find role!");
            role = await _roleManager.FindByIdAsync(roleID);
            if (role != null)
            {
                Input = new InputModel
                {
                    Name = role.Name
                };
                return Page();
            }
            return NotFound("Can't find role!");

        }
        public async Task<IActionResult> OnPostAsync(string roleID)
        {
            if (roleID == null) return NotFound("Can't find role!");
            role = await _roleManager.FindByIdAsync(roleID);
            if (role==null)
            {
                return NotFound("Can't find role!");
            }
            if (!ModelState.IsValid)
            {
                return Page();
            }
            role.Name = Input.Name;
            var result = await _roleManager.UpdateAsync(role);

           
            if(result.Succeeded)
            {
                StatusMessage = $"Updated successfully role name: {Input.Name}";
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
