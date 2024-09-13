using SoundWave.Server.Entities;
using Microsoft.EntityFrameworkCore;

namespace SoundWave.Server.Data
{
    public class DataContext : DbContext
    {

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Song> Songs { get; set; }
        public DbSet<Album> Albums { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>().HasData(
                new User()
                {
                    UserId = 1,
                    Name = "Test_user"
                }
                );
            
            modelBuilder.Entity<Album>().HasData(
                new Album()
                {
                    Id = 1,
                    Name = "Test_album"
                }
                );
        }

    }
}
