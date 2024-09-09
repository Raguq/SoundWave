using SoundWave.Server.Entities;
using Microsoft.EntityFrameworkCore;

namespace SoundWave.Server.Data
{
    public class DataContext : DbContext
    {

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Cinema> Cinemas { get; set; }
        public DbSet<Album> Authors { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new Cinema.Configuration());
            modelBuilder.ApplyConfiguration(new Album.Configuration());

            base.OnModelCreating(modelBuilder);
        }

    }
}
