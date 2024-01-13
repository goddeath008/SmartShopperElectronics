// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyProWeb.Models.Domain;

namespace MyProWeb.Areas.Admin.Pages.User
{
    public class AddRoleModel : PageModel
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AddRoleModel(
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager= roleManager;
        }

       
        [TempData]
        public string StatusMessage { get; set; }

        
        public AppUser user { get; set; }
        [BindProperty]
        [DisplayName("Roles for user")]
        public string[] RoleNames {  get; set; }
        public SelectList allRoles { get; set; }
        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound($"Not exit user!");
            }
            user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound($"Not found user: {id}");
            }
            RoleNames= (await _userManager.GetRolesAsync(user)).ToArray<string>();
            List<string> roleNames = await _roleManager.Roles.Select(a => a.Name).ToListAsync();
            allRoles = new SelectList(roleNames);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string id)
        {

            if (string.IsNullOrEmpty(id))
            {
                return NotFound($"Not exit user!");
            }
            
            user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound($"Not found user: {id}");
            }
            if (!ModelState.IsValid)
            {
                return Page();
            }

            //RoleNames
            var OldRoleNames = (await _userManager.GetRolesAsync(user)).ToArray();
            var DeleteRoles = OldRoleNames.Where(r => !RoleNames.Contains(r));
            var AddRoles = RoleNames.Where(r => !OldRoleNames.Contains(r));
            List<string> roleNames = await _roleManager.Roles.Select(a => a.Name).ToListAsync();
            allRoles = new SelectList(roleNames);
            var resultDelete = await _userManager.RemoveFromRolesAsync(user,DeleteRoles);
            if (!resultDelete.Succeeded)
            {
                resultDelete.Errors.ToList().ForEach(error =>
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                });
                return Page();
            }
            var resultAddRole= await _userManager.AddToRolesAsync(user, AddRoles);
            if (!resultAddRole.Succeeded)
            {
                resultDelete.Errors.ToList().ForEach(error =>
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                });
                return Page();
            }





            StatusMessage = $"Update Role has been set for user: {user.UserName}.";

            return RedirectToPage("./Index");
        }
    }
}
