using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyProWeb.Models.Domain;

namespace MyProWeb.Data
{
    public class AuThenDbContext : IdentityDbContext<AppUser>
    {
        public AuThenDbContext(DbContextOptions<AuThenDbContext> options):base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
