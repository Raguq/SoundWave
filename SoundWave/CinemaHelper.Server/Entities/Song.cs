using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SoundWave.Server.Entities
{
    public class Song
    {
        public int Id { get; set; }
        public string Title { get; set; } = "";
        public int Lenght { get; set; }
        public Album? Album { get; set; }
        public int AlbumId { get; set; }
        public User? UserId{ get; set; }
        DateTime CreatedAt { get; set; }
        DateTime? LastUpdatedAt { get; set; }
    }
}
