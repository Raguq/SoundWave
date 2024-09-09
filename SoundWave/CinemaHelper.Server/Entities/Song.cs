using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SoundWave.Server.Entities
{
    public class Song
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public int AlbumId { get; set; }
        public Album? Author { get; set; }

    }
}
