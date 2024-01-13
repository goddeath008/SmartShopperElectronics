using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Build.ObjectModelRemoting;
using Microsoft.EntityFrameworkCore;
using MyProWeb.Data;
using MyProWeb.Models;
using MyProWeb.Models.Domain;


namespace MyProWeb.Areas.Admin.Pages.User
{
    public class IndexModel : PageModel
    {
        private readonly UserManager<AppUser> _userManager;

        public IndexModel(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }
        [TempData]
        public string StatusMessage {  get; set; }
        public class RoleAndUser: AppUser
        {
            public string RoleNames { get; set;}
        }
        public List<RoleAndUser> users { get; set; }
        public const int ITEMS_PER_PAGE = 10;
        [BindProperty(SupportsGet =true, Name ="p")]
        public int CurrentPage {  get; set; }
        public int CountPages {  get; set; }
        public int totalUsers {  get; set; }
        public async Task OnGet()
        {
            /*users = await _userManager.Users.OrderBy(a => a.UserName).ToListAsync();*/
            var qr = _userManager.Users.OrderBy(a => a.UserName);
            totalUsers = await qr.CountAsync();
            CountPages = (int)Math.Ceiling((double)totalUsers/ITEMS_PER_PAGE);
            if (CurrentPage < 1)
                CurrentPage = 1;
            if (CurrentPage > CountPages)
                CurrentPage = CountPages;
            var qr1 = qr.Skip((CurrentPage - 1) * ITEMS_PER_PAGE)
                    .Take(ITEMS_PER_PAGE)
                    .Select(u=> new RoleAndUser()
                    {
                        Id = u.Id,
                        UserName = u.UserName,
                    });

            users = await qr1.ToListAsync();
            foreach(var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                user.RoleNames = string.Join(",", roles);
            }
 ;      }
        public void OnPost() => RedirectToPage();
    }
}
