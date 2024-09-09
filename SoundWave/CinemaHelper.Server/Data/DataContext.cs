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

    }
}
