using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyProWeb.Data;
using System.ComponentModel.DataAnnotations;

namespace MyProWeb.Areas.Admin.Pages.Role
{
    public class CreateModel : RolePageModel
    {
        public CreateModel(RoleManager<IdentityRole> roleManager, AuthenDbContext context) : base(roleManager, context)
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
        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if(!ModelState.IsValid)
            {
                return Page();
            }

            var newRole = new IdentityRole(Input.Name);
            var result=await _roleManager.CreateAsync(newRole);
            if(result.Succeeded)
            {
                StatusMessage = $"Created successfully a new role: {Input.Name}";
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
