using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyProWeb.Models.Domain;

namespace MyProWeb.Data
{
    public class AuthenDbContext :IdentityDbContext<AppUser>
    {
        public AuthenDbContext(DbContextOptions<AuthenDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
    }
}
