using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using User.Domain.Entities;
using User.Infrastructure.Identity;

namespace User.Infrastructure.Persistence
{
    public class UserDBContext : IdentityDbContext<ApplicationUser,ApplicationRole,Guid>
    {
        public UserDBContext(DbContextOptions<UserDBContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder); 



            var adminRoleId = Guid.NewGuid();
            var userRoleId = Guid.NewGuid();

            builder.Entity<ApplicationUser>()
          .HasMany(u => u.Addresses);

        }




        public DbSet<Address> Addresses { get; set; }

        public DbSet<RefreshToken> RefreshTokens { get; set; }


    }
}
