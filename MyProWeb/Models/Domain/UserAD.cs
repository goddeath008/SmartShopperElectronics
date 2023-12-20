using Microsoft.AspNetCore.Identity;
namespace MyProWeb.Models.Domain
{
    public class UserAD:IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

    }
}
