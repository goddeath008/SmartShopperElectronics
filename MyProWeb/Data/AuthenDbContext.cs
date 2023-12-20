using MyProWeb.Models.Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MyProWeb.Data
{
    public class AuthenDbContext : IdentityDbContext<UserAD>
    {
        public AuthenDbContext(DbContextOptions<AuthenDbContext> options):base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
