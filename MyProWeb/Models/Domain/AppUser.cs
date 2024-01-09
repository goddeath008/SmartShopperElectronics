using Microsoft.AspNetCore.Identity;

namespace MyProWeb.Models.Domain
{
    public class AppUser: IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
