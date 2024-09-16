using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SoundWave.Server.Entities;

namespace SoundWave.Server.Data
{
    public class AuthDataContext : IdentityDbContext<AuthUser>
    {
        public AuthDataContext(DbContextOptions<AuthDataContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

                builder.Entity<AuthUser>().Property(u => u.Initials).HasMaxLength(5);

            builder.HasDefaultSchema("Identity");
        }
    }
}
